let n = Number(localStorage.getItem("ProductID"));
const url = `https://localhost:44367/api/Products/${n}`;

var form = document.getElementById("form");

async function editPro() {
    event.preventDefault();
    let formData = new FormData(form);
    console.log(formData);

    let response = await fetch(url, {
        method: 'PUT',
        body: formData,
        // headers: {
        //     'Content-Type': 'application/json'
        // }
    });
    alert("Success");
    ProductName.innerHTML = "";
}














// const n = localStorage.getItem("ProductID");

// const url = "https://localhost:44367/api/Products/product/" + n;
// console.log(url);

// async function getCategory() {

//     let response = await fetch(url);
//     let result = await response.json();

    


//     let container = document.getElementById("container");

//         container.innerHTML = 
//         `
//         <div class="container mt-5">
//             <div class="row justify-content-center">
//                 <div class="col-md-6">
//                     <div class="card shadow-lg">
//                         <div class="card-body">
//                             <form>
//                                 <div class="form-group">
//                                     <label for="productId">Product ID</label>
//                                     <input type="text" class="form-control" id="productId" name="productId" value="${result.productId}" readonly>
//                                 </div>
//                                 <div class="form-group mt-3">
//                                     <label for="productName">Product Name</label>
//                                     <input type="text" class="form-control" id="productName" name="productName" value="${result.productName}">
//                                 </div>
//                                 <div class="form-group mt-3">
//                                     <label for="price">Price</label>
//                                     <input type="number" class="form-control" id="price" name="price" value="${result.price}">
//                                 </div>
//                                 <div class="form-group mt-3">
//                                     <label for="description">Description</label>
//                                     <textarea class="form-control" id="description" name="description" rows="3">${result.description}</textarea>
//                                 </div>
//                                 <div class="form-group mt-3">
//                                     <label for="productImage">Product Image</label>
//                                     <input type="file" class="form-control-file" id="productImage" name="productImage">
//                                 </div>
//                                 <button type="submit" class="btn btn-primary mt-4 w-100">Submit</button>
//                             </form>
//                         </div>
//                     </div>
//                 </div>
//             </div>
//         </div>
//         `
       
//     // })

// }

// function reset(){
//     localStorage.clear();
// } 

// //  <a class="btn btn-primary" href="details.html" onclick="store(${element.categoryId})">Store Data</a>
// // function store(id){
// //     localStorage.setItem("CProductID",id);
// // }
// getCategory();
