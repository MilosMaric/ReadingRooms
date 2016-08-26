app.controller('registerController', ['$scope', '$window', 'userService', 'facultyService',
                function($scope, $window, userService, facultyService){

    function init() {
      $scope.$parent.checkSession();
      $scope.user = {};
      $scope.isOccupied = false;
      $scope.getAllUsers();
      facultyService.getAll().then(
        function(response) {
          if(response.data) {
            $scope.faculties = response.data;
          }
        }
      );
    }

    $scope.getAllUsers = function() {
      userService.getAll().then(
        function(response) {
          if(response.data) {
            $scope.users = response.data;
          }
        }
      );
    }

    init();

    $scope.checkUsername = function() {
      if($scope.users && $scope.users.length && $scope.user.Username && $scope.user.Username.trim().length > 0) {
        for(var i = 0; i < $scope.users.length; i++) {
          if($scope.users[i].Username === $scope.user.Username) {
            $scope.isOccupied = true;
            return true;
          }
        }
      }
      $scope.isOccupied = false;
      return false;
    }

    $scope.register = function() {
      if($scope.isFormValid()) {
        $scope.user.Role = "Student";
        userService.add($scope.user).then(
          function(response) {
            if(response.data) {
              alert("Registracija je uspešna. Bićete prebačeni na stranicu za prijavljivanje.");
              $window.location.href = "#/login";
            } else {
              alert("Došlo je do greške prilikom registracije.");
            }
          }
        );
      }
    }

    $scope.isFormValid = function() {
      var user = $scope.user;
      if(!$scope.checkUsername() && user.Password && user.Password.trim().length > 0 && user.FacultyId &&
          user.Index && user.Index.trim().length > 0) {
        return true;
      }

      alert("Sva polja moraju biti popunjena i korisničko ime jedinstveno. Nije jedinstveno ako je polje za unos uokvireno crveno!");
      return false;
    }

}]);
