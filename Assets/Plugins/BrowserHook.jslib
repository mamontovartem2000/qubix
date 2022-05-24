mergeInto(LibraryManager.library, {

ReadyToStart: function() {
        ReadyToStartFunction();
    },

GameCancelled: function() {
        GameCancelledFunction();
    },

GameIsOver: function() {
        GameIsOverFunction();
    },

GameError: function() {
        GameErrorFunction();
    },

WorldDestroyed: function() {
        WorldDestroyedFunction();
    },

});
