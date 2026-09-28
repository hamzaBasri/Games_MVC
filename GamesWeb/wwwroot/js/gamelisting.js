var dataTable;
$(document).ready(function() {
    loadDataTable();
});
var gameId = new URLSearchParams(window.location.search).get("gameId");
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { "url": `/Admin/GameListing/GetAll?gameId=${gameId}` },
        "columns": [
            {
                "data": "imageUrl",  
                "render": function (data, type, row) {
                    return `<img src="${data}" alt="${row.platform.name}" style="width:100px;height:60px;object-fit:cover;" />`;
                },
                "width": "15%"
            },
            { "data": "platform.name", "width": "10%" },
            { "data": "priceEBGames", "width": "20%" },
            { "data": "priceAmazon", "width": "20%" },
            { "data": "priceWalmart", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/gamelisting/upsert?gameListingId=${data}&gameId=${gameId}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Modifier
                        </a>
                        <a onClick=Delete('/admin/gamelisting/delete/${data}') class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Supprimer
                        </a>
                    </div>`;
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Êtes-vous sûr?',
        text: "Vous ne pourrez pas annuler cette action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Oui, supprimer!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        Swal.fire(data.message, "", "success");
                    }
                    else {
                        Swal.fire(data.message, "", "error");
                    }

                },
                error: function (xhr, status, error) {
                    Swal.fire("Une erreur s'est produite lors de la suppression.", "", "error");
                }
            });
        }
    });
}
