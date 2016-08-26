app.controller('loginController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init() {
      $scope.$parent.checkSession();
      $scope.$parent.closeMsnger();
      if($scope.$parent.user) {
        $window.location.href = "#/profile";
      }
    }

    init();

    $scope.login = function() {
      if($scope.formIsValid()) {
        userService.login($scope.user).then(
          function(response) {
            if(response.data.Success)
            {
              $scope.$parent.user = response.data;
              localStorage.token = response.data.Token;
              $window.location.href = "#/profile";
            } else {
              $scope.$parent.showMsg("ERROR", "Unešeni su neispravni podaci.");
            }
          }
        );
      }
    }

    $scope.formIsValid = function() {
      if($scope.user && $scope.user.Username && $scope.user.Username.trim().length > 0 &&
          $scope.user.Password && $scope.user.Password.trim().length > 0){
            return true;
      }

      $scope.$parent.showMsg("ERROR", "Oba polja moraju biti popunjena.");

      return false;
    }
}]);
