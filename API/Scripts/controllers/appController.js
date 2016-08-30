app.controller('appController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    var init = function() {
      $scope.checkSession()
    }

    $scope.checkSession = function(callback) {
      var token = $window.localStorage.token;

      if(!$scope.user) {
        if(!token) {
          if(!$window.location.href.endsWith("#/register")) {
            $window.location.href = "#/login";
          }
        } else {
          userService.getLoggedUser().then(
            function(response) {
              if(response.data){
                $scope.user = { Role: response.data.Role};
                if(callback) {
                  callback();
                }
                if($window.location.href == "#/login" || $window.location.href == "#/register") {
                  $window.location.href = "#/profile";
                }
              } else {
                if($window.location.href != "#/login" || $window.location.href != "#/register") {
                  $window.location.href = "#/login";
                }
              }
            }
          );
        }
      } else if(callback) {
        callback();
      }
    }

    init();

    $scope.showMsg = function(type, content) {
      $scope.msgType = type;
      $scope.msg = content;
    }

    $scope.closeMsnger = function() {
      $scope.msg = undefined;
    }

    $scope.openModal = function(msg, okCallback, cancelCallback) {
      $scope.confirm = {};
      $scope.confirm.msg = msg;
      $scope.confirm.okCallback = okCallback;
      $scope.confirm.cancelCallback = cancelCallback;
    }

    $scope.confirmOk = function() {
      if($scope.confirm.okCallback) {
        $scope.confirm.okCallback();
      }
      $scope.confirm = {};
    }

    $scope.confirmCancel = function() {
      if($scope.confirm.cancelCallback) {
        $scope.confirm.cancelCallback();
      }
      $scope.confirm = {};
    }

    $scope.logout = function() {
      $scope.user = undefined;
      localStorage.clear();
      $window.location.href = "#/login";
    }
}]);
