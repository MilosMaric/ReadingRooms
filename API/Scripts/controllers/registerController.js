app.controller('registerController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init() {
      $scope.$parent.checkSession();
    }

    init();

}]);
