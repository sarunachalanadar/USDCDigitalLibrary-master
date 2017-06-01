var app = angular.module("app", ["ngRoute"]);
app.config(function ($routeProvider, $locationProvider) {
	var templatesDir = "/Templates/";
	$routeProvider
    .when("/", {
    	templateUrl: templatesDir + "Index.html",
    	controller: "homeController"
    })
    .when("/home", {
    	templateUrl: templatesDir + "Index.html",
    	controller: "homeController"
    })
    .when("/test", {
    	templateUrl: templatesDir + "Test.html",
    	controller: "testController"
    })
    .otherwise({
    	redirectTo: "/"
    });
});

app.controller('homeController', function ($scope, apiService) {
	$scope.message = "This message is from the Home controller";

	$scope.testGet = function () {
		apiService.GetRequest("test", "2");
	};

	$scope.testPost = function () {
		apiService.PostRequest("test", "3");
	}
});

app.controller('testController', function ($scope) {
	$scope.message = "This message is from the Test controller";
});

app.factory("apiService", function ($http) {
	var url = "/api/";

	return {
		GetRequest: function (path, parameters) {
			$http({
				method: 'GET',
				url: url + path + "/" + parameters
			})
            .then(function successCallback(response) {
            	alert(response.data);
            },
            function errorCallback(response) {
            	alert("Error");
            });
		},
		PostRequest: function (path, parameters) {
			$http({
				method: 'POST',
				url: url + path,
				data: parameters
			})
			.then(function successCallback(response) {
				alert(response.data);
			},
			function errorCallback(response) {
				alert("Error");
			});
		}
	}
});