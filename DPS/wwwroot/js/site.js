// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
 
// Write your JavaScript code.


GetProduct();
function GetProduct() {
    function getSweetList() {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", "/Home/GetSweet"); // Sostituisci "/dolci" con l'URL effettivo dell'endpoint
      
        xhr.onload = function() {
          if (xhr.status === 200) {
            var sweets = JSON.parse(xhr.responseText); // Parsing manuale della risposta JSON
            console.log(sweets);
            var result = ''; 
            var i = 1;
            sweets.forEach(element => {
                result += '<tr>';
                result += '<th>' + i + '</th>';
                result += '<th>' + element.nome + '</th>';
                result += '<th>' + element.prezzo + '</th>';
                result += '<th>' + element.quantita + '</th>';
                result += '<td class="text-center py-1"> <button type="button" class="btn btn-outline-primary btn-xs open-AddBookDialog" id="'+ element.id +'" data-bs-toggle="modal" data-bs-target="#exampleModal" asp-action="Edit" asp-route-id="'+ element.id +'"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-info" viewBox="0 0 16 16"> <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/></svg></button></td>';
                result += '</tr>';
                i++;
            });

            document.getElementById("tblBody").innerHTML = result;
          } else {
            console.error("Errore durante la chiamata:", xhr.statusText);
          }
        };
      
        xhr.onerror = function() {
          console.error("Errore di rete");
        };
      
        xhr.send();
      }
      
      getSweetList();
}

function saveSweet() {
  var formObj = new Object();
  formObj.id = document.getElementById('id').value;
  formObj.nome = document.getElementById('nome').value;
  formObj.prezzo = document.getElementById('prezzo').value;
  formObj.quantita = document.getElementById('quantita').value;
  formObj.data = document.getElementById('data').value; 
  var ingredients = document.getElementById('ConcatIngredients').value;
  formObj.ingredienti = ingredients.split(';'); 

  var formData = new FormData();
  formData.append('id', formObj.id);
  formData.append('nome', formObj.nome);
  formData.append('prezzo', formObj.prezzo);
  formData.append('quantita', formObj.quantita);
  formData.append('data', formObj.data);

  for (var i = 0; i < formObj.ingredienti.length; i++) {
    formData.append('ingredienti[]', formObj.ingredienti[i]);
  }

  var xhr = new XMLHttpRequest();
  xhr.open('POST', 'http://localhost:5154/Home/CreateSweet');
  xhr.onload = function() {
    if (xhr.status === 200) {
      alert('Saved!');
      location.reload();
    } else {
      alert('Error:', xhr.statusText);
    }
  };
  xhr.send(formData);


}