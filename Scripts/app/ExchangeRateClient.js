var ExchangeRateApp = angular.module('ExchangeRateApp', []);

ExchangeRateApp.controller('ExchangeRateController', function ($scope, ExchangeRateService) {

    loadExchangeRates();

    function loadExchangeRates() {
         //Default date - cannot do current date as web service only display data until 2015
         var selectedDate = new Date(2013, 04, 06); 
         $scope.dataLoading = true;

        ExchangeRateService.getExchangeRates(selectedDate)
        .success(function (data) {
            $scope.exchangeRates = data;
            $scope.selectedDate = selectedDate;
            $scope.dataLoading = false;
        })
        .error(function (error) {
            $scope.dataLoading = false;
        });
    }

    $scope.getExchangeRates = function(selectedDate) {
        $scope.dataLoading = true;

        ExchangeRateService.getExchangeRates(selectedDate)
        .success(function (data) {
            $scope.dataLoading = false;
            $scope.exchangeRates = data.CurrencyChangeList;
        })
        .error(function (error) {
            $scope.dataLoading = false;
        });
    }

});

ExchangeRateApp.factory("ExchangeRateService", ['$http', function ($http) {

    var ExchangeRateService = {};

    ExchangeRateService.getExchangeRates = function (selectedDateInput) {
        return $http({ method: 'POST', url: '/ExchangeRate/GetExchangeRates', data: { selectedDate: selectedDateInput } });
    };
    return ExchangeRateService;

}]);
