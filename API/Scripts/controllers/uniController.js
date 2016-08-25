app.controller('uniController', ['$scope', '$window', 'universityService', function($scope, $window, universityService){

    function init() {
      $scope.$parent.checkSession();
      $scope.uni = {};
      $scope.mode = "VIEW";
      universityService.getAll().then(
        function(response) {
          if(response.data.length){
            $scope.unis = response.data;
          }
        }
      );
    };

    init();

    $scope.addMode = function() {
      $scope.mode = "ADD";
      $scope.uni = {};
    };

    $scope.editMode = function(uni, index) {
      $scope.mode = "EDIT";
      $scope.uni = angular.copy(uni);
      $scope.index = index;
    };

    $scope.viewMode = function() {
      $scope.mode = "VIEW";
      $scope.uni = {};
    };

    $scope.delete = function(id, idx) {
      if(idx) {
        universityService.delete(id).then(
          function() {
            $scope.unis.splice(idx, 1);
          }
        );
      }
    }

    $scope.add = function() {
      universityService.add($scope.uni).then(
        function(response) {
          if(response.data) {
            $scope.unis.push(response.data);
            $scope.viewMode();
          } else {
            alert("Došlo je do greške prilikom dodavanja.");
          }
        }
      );
    }

    $scope.edit = function() {
      universityService.update($scope.uni.Id, $scope.uni).then(
        function(response) {
          $scope.unis[$scope.index] = $scope.uni;
          $scope.viewMode();
        }
      );
    }

    $scope.refresh = function() {
      universityService.getAll().then(
        function(response) {
          if(response.data.length){
            $scope.unis = response.data;
          }
        }
      );
    }
}]);
