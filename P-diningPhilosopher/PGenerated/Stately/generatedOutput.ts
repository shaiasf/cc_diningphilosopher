import { createMachine, assign } from 'xstate';
interface Context {retries: number;}
const Fork = createMachine<Context>({
        id: "Fork",
        initial: "Available", 
        states: {
            Available: {
                on: {
                    ePickUp : { target: [
                        "Taken",
                        ]
                    },
                }
            },
            Taken: {
                on: {
                    ePutDown : { target: [
                        "Available",
                        ]
                    },
                    ePickUp : { target: [
                        "Taken",
                        ]
                    },
                }
            }
        }
});
const Philosopher = createMachine<Context>({
        id: "Philosopher",
        initial: "Init", 
        states: {
            Init: {
                always: [
                { target: [
                    "Thinking",
                    ]
                }
                ],
            },
            Thinking: {
                on: {
                    eStart : { target: [
                        ]
                    },
                    eThink : { target: [
                        "Hungry",
                        ]
                    },
                }
            },
            Hungry: {
                on: {
                    eTaken : { target: [
                        "Eating",
                        ]
                    },
                    eBusy : { target: [
                        "Hungry",
                        ]
                    },
                }
            },
            Eating: {
                on: {
                    eThink : { target: [
                        "Thinking",
                        ]
                    },
                }
            }
        }
});
const TestWithNoSwap = createMachine<Context>({
        id: "TestWithNoSwap",
        initial: "Init", 
        states: {
            Init: {
            }
        }
});
const TestWithSwap = createMachine<Context>({
        id: "TestWithSwap",
        initial: "Init", 
        states: {
            Init: {
            }
        }
});
