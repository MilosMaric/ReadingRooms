app.controller('facultyController', ['$scope', '$window', function($scope, $window){

    function init() {
      $scope.$parent.checkSession();
      if(!$scope.$parent.user) {
        $window.location.href = "#/login";
      } else if($scope.$parent.user.Role == "Student"){
          $window.location.href = "#/profile";
      }
    }


    init();

}]);
