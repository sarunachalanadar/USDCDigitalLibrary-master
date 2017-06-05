var app = angular.module("app", ["ngRoute"]);
app.config(function ($routeProvider, $locationProvider) {
	var templatesDir = "/Templates/";
	$routeProvider
    .when("/", {
    	templateUrl: templatesDir + "Index.html",
    	controller: "homeController"
    })
    //.when("/home", {
    //	templateUrl: templatesDir + "Index.html",
    //	controller: "homeController"
    //})
    .when("/test", {
        templateUrl: templatesDir + "Test.html",
        controller: "testController"
        })
    .when("/dashboard", {
        templateUrl: templatesDir + "Dashboard.html",
        controller: "dashboardController"
        })
    .when("/search", {
        templateUrl: templatesDir + "Search.html",
        controller: "searchController"
    })
    .otherwise({
    	redirectTo: "/"
    });
});



app.controller('homeController', function ($scope, apiService, $location, $http) {
    //$scope.formData = {};
    $scope.message = "This message is from the Home controller";
    //$scope.isAuthenticated = false;
    //$scope.isAdmin = false;

    initialize();

    //$scope.buttonClick = function () {
    //    apiService.GetRequest("test", "2");
    //};
    
    $scope.login = function () {

        $scope.Authenticate();          

    };
    //$scope.isActive = function (viewLocation) {
    //    return viewLocation === $location.path();
    //};

    //$scope.$on('$locationChangeSuccess', function (/* EDIT: remove params for jshint */) {
    //    if ($scope.isAuthenticated == true) {
    //        $location.path('/dashboard');
    //    }
    //});


    //Authenticate based on username and password
    $scope.Authenticate = function () {
        $http.post('/api/Solr', '"' + 'UserName:' + $scope.formData.username + ' AND Password:' + $scope.formData.password + '"').
            then(function (response) {
                console.log(response.data);
                if (response.data.length == 1) {
                    $scope.isAuthenticated = true;
                    $location.path('/dashboard');
                    $scope.username = response.data[0].UserName;
                    if (response.data[0].RoleTypeDesc == 'Admin'){
                        $scope.isAdmin = true;                        
                    }
                    $scope.GetItems();
                }
                else {
                                          
                    
                }
                
            },
            function (response) {
                alert("Issue with POST to SOLR")
            });
    }


    //GetItems for Dashboard
    $scope.GetItems = function () {
        var queryString;
        if ($scope.isAdmin == true) {
            queryString = '"' + 'DataTableName:Items AND Loaned:Yes' + '"'
        }
        else {
            queryString = '"' + 'DataTableName:Items AND UserName:'+$scope.username + '"'
        }

        $http.post('/api/Solr', queryString).
            then(function (response) {                
                console.log(response.data);
                $scope.items = response.data;
            },
            function (response) {                
                alert("Issue with POST to SOLR")
            });
    }

    function initialize () {
        $scope.formData = {};
        $scope.q = {};
        $scope.isAdmin = false;
        $scope.isAuthenticated = false;

    }


    $scope.Search = function () {
        console.log("From HomeController")
        $location.path('/search');

        var queryString;

        if ($scope.q.queryString != undefined && $scope.q.queryString != ''){
            queryString = '"' + $scope.q.queryString + ' AND DataTableName:Items' + '"';
        }
        else {
            queryString = '"' + 'DataTableName: Items' + '"';
        }
        $http.post('/api/Solr', queryString).
            then(function (response) {
                console.log(response.data);
                $scope.SearchResults = response.data;
            },
            function (response) {
                alert("Issue with POST to SOLR")
            });
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
                alert("Success");
            },
            function errorCallback(response) {
                alert("Failure");
            });
        }
    }
});

app.controller('dashboardController', function ($scope) {
    console.log("In dashboard controller");

});

app.controller('searchController', function ($scope) {
    console.log("In search controller");


});

