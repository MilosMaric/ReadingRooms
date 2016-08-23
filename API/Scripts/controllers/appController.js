app.controller('appController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init()
    {
        if(!$scope.user){
          $window.location.href = "#/login";
        } else {
          $window.location.href = "#/profile";
        }
    }

    $scope.checkSession = function() {
      var token = $window.localStorage.token;
      if(token) {
        userService.getLoggedUser().then(
          function(response) {
            if(response.data){
              $scope.user = { Role: response.data.Role};
            } else {
              $window.location.href = "#/login";
            }
          }
        );
      }
    }

    init();

    $scope.logout = function() {
      $scope.user = undefined;
      localStorage.clear();
    }
}]);
