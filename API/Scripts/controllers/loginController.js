app.controller('loginController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init() {
      $scope.$parent.checkSession();
      if($scope.$parent.user) {
        $window.location.href = "#/profile";
      }
    }

    init();

    $scope.login = function() {
      userService.login($scope.user).then(
        function(response) {
          if(response.data.Success)
          {
            $scope.$parent.user = response.data;
            localStorage.token = response.data.Token;
            $window.location.href = "#/profile";
          } else {
            alert("Unešeni su neispravni podaci.");
          }
        }
      );
    }
}]);
