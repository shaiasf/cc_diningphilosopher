// ==========================
// Events
// ==========================
event ePickUp: machine;
event ePutDown: machine;
event eTaken: machine;
event eBusy: machine;
event eThink;
event eEat;
event eStart;
event eDelay;





// ==========================
// Fork Machine
// ==========================
machine Fork {
  var inUse: bool;
  var holder: machine;

  start state Init {
    entry {
      inUse = false;
    }
    on ePickUp do handlePickUp;
    on ePutDown do handlePutDown;
  }

  fun handlePickUp(p: machine) {

  if (!inUse) {
    inUse = true;
    holder = p;
    send p, eTaken, this;
    announce ForkGranted, (philosopher = p, fork = this);
  } else {
    send p, eBusy, this;
  }
}

fun handlePutDown(p: machine) {
  if (holder == p) {
    inUse = false;
    announce ForkReleased, (philosopher = p, fork = this);
    holder = null;
  }
}

}

// ==========================
// Philosopher Machine
// ==========================
machine Philosopher {
  var leftFork: machine;
  var rightFork: machine;
  var hasLeft: bool;
  var hasRight: bool;

  start state Init {
  entry (payload: (machine, machine)) {
    leftFork = payload.0;
    rightFork = payload.1;
    hasLeft = false;
    hasRight = false;
  }
  on eStart do startEating;
}


  state ReadyToEat {
  entry {
    goto Hungry;
  }
}


state Thinking {
  entry {
    raise eDelay;
  }
  on eDelay goto ReadyToEat;
}


  state Hungry {
    entry {
      send leftFork, ePickUp, this;
    }
    on eTaken do onTaken;
    on eBusy do retry;


  }

  state FinishEating {
  entry {
    send leftFork, ePutDown, this;
    send rightFork, ePutDown, this;
    hasLeft = false;
    hasRight = false;
  }
}

state Eating {
  entry {
    raise eDelay;
  }
  on eDelay goto  FinishEating;
}



  fun goHungry() {
    goto Hungry;
  }

  fun goThinking() {
    goto Thinking;
  }

  fun onTaken(f: machine) {
    if (f == leftFork) {
      hasLeft = true;
      send rightFork, ePickUp, this;
    } else if (f == rightFork) {
      
      hasRight = true;
      goto Eating;
    }
  }

  fun retry(f: machine) {
    if (hasLeft && !hasRight) {
        send leftFork, ePutDown, this;
        hasLeft = false;
    } else if (hasRight && !hasLeft) {
        send rightFork, ePutDown, this;
        hasRight = false;
    }
    goto Thinking; // go back and delay before retrying
}

fun startEating() {
  goto Hungry;
}

}

// ==========================
// Test Harness - No Swap
// ==========================
machine TestWithNoSwap {
  start state Init {
    entry {
      var forks: map[int, machine];
      var phils: map[int, machine];
      var i: int;
      var left: machine;
      var right: machine;
      var phil: machine;

      i = 0;
      while (i < 5) {
        forks[i] = new Fork();
        i = i + 1;
      }

      i = 0;
      while (i < 5) {
        left = forks[i];
        right = forks[(i + 1) % 5];
        phil = new Philosopher((left, right));
        phils[i] = phil;
        i = i + 1;
      }

      i = 0;
      while (i < 5) {
        send phils[i], eStart;
        i = i + 1;
      }
    }
  }
}

// ==========================
// Test Harness - With Swap
// ==========================
machine TestWithSwap {
  start state Init {
    entry {
      var forks: map[int, machine];
      var phils: map[int, machine];
      var i: int;
      var left: machine;
      var right: machine;
      var phil: machine;

      i = 0;
      while (i < 5) {
        forks[i] = new Fork();
        i = i + 1;
      }

      i = 0;
      while (i < 5) {
        left = forks[i];
        right = forks[(i + 1) % 5];

        if (i == 0) {
          phil = new Philosopher((right, left)); // Swap to break circular wait
        } else {
          phil = new Philosopher((left, right));
        }

        phils[i] = phil;
        i = i + 1;
      }

      i = 0;
      while (i < 5) {
        send phils[i], eStart;
        i = i + 1;
      }
    }
  }
}

// ==========================
// Tests
// ==========================
test TestCaseNoSwap [main=TestWithNoSwap]:
   assert ForkUsageSpec in 
   (union Fork, Philosopher, { TestWithNoSwap }); 

test TestCaseWithSwap [main=TestWithSwap]:
   assert ForkUsageSpec in 
   (union Fork, Philosopher, { TestWithSwap });
