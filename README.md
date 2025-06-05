📦 # Dining Philosophers in Akka & P (UM6P - Cloud Computing 2025)

This project implements the classic Dining Philosophers Problem using two concurrency frameworks:

Akka (Scala): Models each philosopher and fork as separate actors. Includes strategies to prevent deadlock and ensure each philosopher eats.
P Language: Includes two variants—one incorrect (with deadlock) and one corrected. Comes with formal specifications to detect deadlock and verify correctness using the P model checker.

## Implementation of the Dining Philosophers in Akka 

The implementation of the Dining Philosophers in Akka uses at least 5 philosopher and forks, each represented as a seperate actor.

**How to Run?**
 1. Clone the Github Repository and navigate to akka-diningPhilosophers
 2. To start the simulation with the default number of philosopher use `sbt run `
 3. To use a specific number of philosophers/forks (e.g. 7 philosophers & forks ) use `sbt run --count 7

**How is Deadlock avoided?**

In the classic deadlock scenario, all philosophers pick up their left fork and wait for the right one → circular blocking.
By reversing the order for one philosopher, at least one philosopher is trying to acquire forks in the opposite order. This allows at least one fork to be released, breaking the deadlock chain.

Philosophers 1 to n−1 always pick up their left fork first, then the right fork. Philosopher n picks up the right fork first, then the left fork. This breaks the circular wait condition, one of the four necessary conditions for deadlock to occur.

**Expected outcome of running the program** 

When running the progam, the following behavior should be observed:
  1. Each philosopher starts by thinking, then becomes hungry.
  2. Each philosopher attempts to pick up two forks ( one at a time ).
  3. When both forks are acquired, the philosopher enters the eating state.
  4. After eating, the philosopher puts down both forks and starts thinking again.
  5. This cycle continues indefinitely, or until you terminate the program manually.

![image](https://github.com/user-attachments/assets/e5fe437d-7473-4d15-89bc-3eadbeb5d3db)

