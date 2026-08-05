var dataTable;

$(document).ready(function () {
    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/game/getall' },
        "columns": [
            { data: 'title', "width": "10%" },
            { data: 'description', "width": "10%" },
            { data: 'producer', "width": "10%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'priceWalmart', "width": "10%" },
            { data: 'priceAmazon', "width": "10%" },
            { data: 'priceEBGames', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return`<div class="w-75 btn-group" role="group">
                            <a href="/admin/game/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Modiffier</a>             
                            <a onClick=Delete('/admin/game/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Suprimer</a>             
                     </div>`
                },
                "width": "20%"
            }
        ]
    });

}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

