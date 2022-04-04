$(document).ready(function () {
  bindAgreement();
});

bindAgreement = () => {
  $.ajax({
    type: "POST",
    url: "/Home/GetAgreement",
    data: '{}',
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: OnSuccess,
    failure: function (response) {
      alert(response.d);
    },
    error: function (response) {
      alert(response.d);
    }
  });
}

function OnSuccess(response) {
  $("#example").DataTable(
    {
      bLengthChange: true,
      lengthMenu: [[5, 10, -1], [5, 10, "All"]],
      bFilter: true,
      bSort: true,
      bPaginate: true,
      data: JSON.parse(response),
      select: true,
      columns: [
        { 'data': 'UserName', title: 'UserName', tooltip: 'UserId' },
        {
          'data': 'GroupCode', title: 'Group Code', "render": function (data, type, full, meta) {
            return '<span data-toggle="tooltip" title="' + full.GroupDescription + '">' + data + '</span>';
        } },
        {
          'data': 'ProductNumber', title: 'Product Number', "render": function (data, type, full, meta) {
            return '<span data-toggle="tooltip" title="' + full.ProductDescription + '">' + data + '</span>';
          }
        },
        { 'data': 'EffectiveDate', title: 'Effective Date', render: $.fn.dataTable.render.moment('YYYY-MM-DDTHH:mm:ss','MM/DD/YYYY') },
        { 'data': 'ExpirationDate', title: 'Expiration Date', render: $.fn.dataTable.render.moment('YYYY-MM-DDTHH:mm:ss', 'MM/DD/YYYY') },
        { 'data': 'ProductPrice', title: 'Product Price' },
        { 'data': 'NewPrice', title: 'New Price' },
        {
          data: null,
          "render": function (data, type, row, meta) {
            return "<a href='javascript:;' class='btn btn-primary' onclick=showInPopup('" + row.Id + "'); >Edit</a>";
          }
        },
        {
          data: null,
          render: function (data, type, row) {
            return "<a href='javascript:;' class='btn btn-danger' onclick=jQueryAjaxDelete('" + row.Id + "'); >Delete</a>";
          }
        },
      ]
    });
};
var forms = null;
validationForm = () => {
  // Fetch all the forms we want to apply custom Bootstrap validation styles to
  forms = document.querySelectorAll('.needs-validation')

  // Loop over them and prevent submission
  Array.prototype.slice.call(forms)
    .forEach(function (form) {
      form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
          event.preventDefault()
          event.stopPropagation()
        }

        form.classList.add('was-validated')
      }, false)
    })
 }

bindProduct = () => {
  $list = $("#Products");
  $.ajax({
    url: "Home/GetProducts",
    type: "GET",
    data: { id: $("#ProductGroups").val() }, //id of the state which is used to extract cities
    traditional: true,
    success: function (result) {
      $list.empty();
      $.each(result, function (i, item) {
        $list.append('<option value="' + item["id"] + '"> ' + item["productNumber"] + ' </option>');
      });
    },
    error: function () {
      alert("Something went wrong call the police");
    }
  });
}

showInPopup = (id = 0) => {
  title = 'Agreement';
  if (id > 0) {
    title = 'Edit Agreement';
	}
  $.ajax({
    type: 'GET',
    url: 'Home/AddOrEdit/' + id,
    success: function (res) {
      $('#agreementModal .modal-dialog').html(res);
      $('#agreementModal .modal-title').html(title);
      $('#agreementModal').modal('show');
      validationForm();
    }
  })
}

jQueryAjaxPost = form => {
  if (form.checkValidity()) {
  try {
      $.ajax({
        type: 'POST',
        url: form.action,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
          if (res.isValid) {
            $('#example').DataTable().destroy();
            bindAgreement();
            $('#agreementModal .modal-dialog').html('');
            $('#agreementModal .modal-title').html('');
            $('#agreementModal').modal('hide');
          }
        },
        error: function (err) {
          console.log(err)
        }
      })
      //to prevent default form submit event
      return false;
    } catch (ex) {
      console.log(ex)
    }
  }
}

jQueryAjaxDelete = id => {
  if (confirm('Are you sure to delete this record ?')) {
    try {
      $.ajax({
        type: 'POST',
        url: 'Home/Delete/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
          $('#example').DataTable().destroy();
          bindAgreement();
        },
        error: function (err) {
          console.log(err)
        }
      })
    } catch (ex) {
      console.log(ex)
    }
  }

  //prevent default form submit event
  return false;
}