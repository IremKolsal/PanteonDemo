var enums = [
    {
        name: 'Farm', value: 1
    },
    {
        name: 'Academy', value: 2
    },
    {
        name: 'Headquarters', value: 3
    },
    {
        name: 'LumberMill', value: 4
    },
    {
        name: 'Barracks', value: 5
    }
]

var loadData = function () {
    var url = '/api/BuildingApi/GetBuilding'
    $.get(url, function (d) {
        $('.datatable-table tbody').html('');
        d.data.forEach(function (item) {
            var buildingTypeName = enums.filter(function (d) {
                return d.value == item.buildingType
            })
            console.log(buildingTypeName)
            var row = '<tr>';
            row += '<td>' + buildingTypeName[0].name + '</td>';
            row += '<td>' + item.buildingCost + '</td>';
            row += '<td>' + item.constructionTime + '</td>';
            // Add more columns here if needed
            row += '</tr>';
            console.log(row)
            $('.datatable-table tbody').append(row);
        });
    })

}
window.onload = function () { loadData() };

var save = function () {
    var buildingType = $('#buildingType').val();
    var buildingCost = $('#inputBuildingCost').val();
    var constructionTime = $('#inputConstructionTime').val();

    if (buildingType == 0) {
        Swal.fire({
            icon: 'error',
            title: 'Hata...',
            text: 'Lütfen Seçim Yapınız!'
        });
        return;
    }
    if (buildingCost == '') {
        Swal.fire({
            icon: 'error',
            title: 'Hata...',
            text: 'Lütfen Building Cost giriniz!'
        });
        return;
    }
    if (constructionTime == '') {
        Swal.fire({
            icon: 'error',
            title: 'Hata...',
            text: 'Lütfen Construction Time Giriniz!'
        });
        return;
    }
    var url = '/api/BuildingApi/AddBuilding'
    var mdl = { BuildingType: buildingType, BuildingCost: buildingCost, ConstructionTime: constructionTime }
    $.post(url, mdl, function (d) {
        console.log(d)
        if (d.isSuccess) {
            $('#exampleModal').modal('hide');
            loadData();
            Swal.fire({
                icon: 'success',
                title: 'Başarılı...',
                text: d.message
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Hata...',
                text: d.message
            });
        }
    })
}

var logout = function () {
    var url = '/api/AccountApi/Logout'
    $.post(url, function (d) {
        window.location.href = '/Account/Login'
    })
}