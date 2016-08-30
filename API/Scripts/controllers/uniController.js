app.controller('uniController', ['$scope', '$window', 'universityService', function($scope, $window, universityService){

    function init() {
      $scope.$parent.checkSession();
      $scope.$parent.closeMsnger();
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
      $scope.deleteInfo = { id : id, idx : idx};
      $scope.$parent.openModal(
        "Ako obrišete univerzitet, brišu se svi fakulteti i studenti koji mu pripadaju. Da li ste sigurni da želite da obrišete univerzitet?",
        $scope.deleteCallback
      );
    }

    $scope.deleteCallback = function() {
      universityService.delete($scope.deleteInfo.id).then(
        function() {
          $scope.unis.splice($scope.deleteInfo.idx, 1);
        }
      );
    }

    $scope.add = function() {
      universityService.add($scope.uni).then(
        function(response) {
          if(response.data) {
            $scope.unis.push(response.data);
            $scope.viewMode();
          } else {
            $scope.$parent.showMsg("ERROR", "Došlo je do greške prilikom dodavanja.");
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
