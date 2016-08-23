app.controller('uniController', ['$scope', '$window', function($scope, $window){

    function init() {
      $scope.$parent.checkSession();
      if(!$scope.$parent.user) {
        $window.location.href = "#/login";
      } else if($scope.$parent.user.Role !== "Administrator"){
          $window.location.href = "#/profile";
      }
    }


    init();

}]);
