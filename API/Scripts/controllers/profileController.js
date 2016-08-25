app.controller('profileController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init() {
      $scope.$parent.checkSession();

      userService.getLoggedUser().then(
        function(response) {
          if(response.data) {
            $scope.loggedUser = response.data;
          }
        }
      );
    }

    init();

}]);
