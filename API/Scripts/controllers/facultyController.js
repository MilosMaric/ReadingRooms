app.controller('facultyController', ['$scope', '$window', function($scope, $window){

    function init() {
      $scope.$parent.checkSession();
    }


    init();

}]);
