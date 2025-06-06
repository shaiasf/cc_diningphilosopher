import akka.actor.{Actor, ActorRef, ActorSystem, Props, PoisonPill}
import scala.concurrent.duration._
import scala.language.postfixOps
import scopt.OParser

// Messages
sealed trait PhilosopherMessage
case object Think extends PhilosopherMessage
case object Eat extends PhilosopherMessage
case object Hungry extends PhilosopherMessage

sealed trait ForkMessage
case class PickUp(philosopher: ActorRef) extends ForkMessage
case class PutDown(philosopher: ActorRef) extends ForkMessage
case class Taken(fork: ActorRef) extends ForkMessage
case class Busy(fork: ActorRef) extends ForkMessage
case class Config(count: Int = 5)


// Fork Actor
class Fork extends Actor {
  var inUse: Boolean = false
  var holder: Option[ActorRef] = None

  def receive: Receive = {
    case PickUp(philosopher) if !inUse =>
      inUse = true
      holder = Some(philosopher)
      philosopher ! Taken(self)

    case PickUp(philosopher) if inUse =>
      philosopher ! Busy(self)

    case PutDown(philosopher) if holder.contains(philosopher) =>
      inUse = false
      holder = None
      println(s"Fork ${self.path.name} put down by ${philosopher.path.name}")

    case _ => // ignore
  }
}

// Philosopher Actor
class Philosopher(leftFork: ActorRef, rightFork: ActorRef) extends Actor {
  import context.dispatcher

  var leftAcquired  = false
  var rightAcquired = false

  def think(): Unit = {
    println(s"${self.path.name} is thinking...")
    context.system.scheduler.scheduleOnce((scala.util.Random.nextInt(5) + 1).seconds, self, Hungry)
  }

  def eat(): Unit = {
    println(s"${self.path.name} starts eating.")
    leftFork ! PutDown(self)
    rightFork ! PutDown(self)
    leftAcquired = false
    rightAcquired = false
    println(s"${self.path.name} Put the fork ${leftFork} and ${rightFork} down")
    context.system.scheduler.scheduleOnce((scala.util.Random.nextInt(5) + 1).seconds, self, Think)
  }

  def busy() : Unit = {

    if (leftAcquired == false){
      leftFork  ! PickUp(self)
    }
    else{
      rightFork ! PickUp(self)
    }



    



  }

  def receive: Receive = {
    case Think =>
      think()

    case Hungry =>
      leftFork  ! PickUp(self)

    

    case Taken(fork) =>
      if (fork == leftFork) {
        leftAcquired = true
        rightFork ! PickUp(self)
      }

      if (fork == rightFork) {
        Thread.sleep(20000)
        rightAcquired = true
        self ! Eat 
      

      }

    case Busy(fork) =>
      // println(s"${self.path.name} found fork busy: ${fork.path.name}")
      busy()
      

    case Eat =>
      eat()
  }
}




object DiningPhilosophersApp extends App {

  // CLI Config case class
  case class Config(count: Int = 5)

  // CLI argument parser
  val builder = OParser.builder[Config]
  val parser = {
    import builder._
    OParser.sequence(
      programName("DiningPhilosophersApp"),
      head("Dining Philosophers", "1.0"),
      opt[Int]('n', "count")
        .action((x, c) => c.copy(count = x))
        .text("number of philosophers and forks (default is 5)")
    )
  }

  // Parse arguments
  OParser.parse(parser, args, Config()) match {
    case Some(config) =>
      val system = ActorSystem("DiningPhilosophersSystem")
      val n = config.count

      // Create forks
      val forks = (1 to n).map(i =>
        system.actorOf(Props[Fork], s"Fork$i")
      )

      // Create philosophers
      val philosophers = (1 to n).map { i =>
        val left  = forks(i - 1)
        val right = forks(i % n)

        val (firstFork, secondFork) =
          if (i == n) (right, left)
          else         (left,  right)

        system.actorOf(Props(new Philosopher(firstFork, secondFork)), s"Philosopher$i")
      }

      // Start simulation
      philosophers.foreach(_ ! Think)

      scala.io.StdIn.readLine()

    case _ =>
      // error already printed by parser
  }
}
