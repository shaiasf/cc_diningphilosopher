// Events observed by the spec
event ForkGranted: (philosopher: machine, fork: machine);
event ForkReleased: (philosopher: machine, fork: machine);

// Spec: Tracks number of philosophers holding at least one fork
spec ForkUsageSpec observes ForkGranted, ForkReleased {
  var activePhilosophers: set[machine];

  start state Monitoring {

    entry{

        
    }
    on ForkGranted do (payload: (philosopher: machine, fork: machine)) {
      activePhilosophers += (payload.philosopher);
      assert sizeof(activePhilosophers) < 5;
    }

    on ForkReleased do (payload: (philosopher: machine, fork: machine)) {
      // Optional: remove philosopher only if no forks are held,
      // but since we're only asked to decrement blindly:
      activePhilosophers -= payload.philosopher;
    }
  }
}
