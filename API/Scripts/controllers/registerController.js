app.controller('registerController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init() {
      $scope.$parent.checkSession();
      if($scope.$parent.user) {
        $window.location.href = "#/profile";
      }
    }

    init();

}]);
