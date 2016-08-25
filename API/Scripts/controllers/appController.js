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

    $scope.logout = function() {
      $scope.user = undefined;
      localStorage.clear();
      $window.location.href = "#/login";
    }
}]);
