app.controller('reservationController', ['$scope', '$window', 'userService', function($scope, $window, userService){

    function init() {
      $scope.$parent.checkSession();
      $scope.$parent.closeMsnger();
    }

    init();


}]);
