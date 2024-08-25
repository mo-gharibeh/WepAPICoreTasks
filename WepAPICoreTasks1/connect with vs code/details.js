const n = localStorage.getItem("ProductID");

const url = "https://localhost:44367/api/Products/product/" + n;
console.log(url);

async function getCategory() {

    let response = await fetch(url);
    let result = await response.json();

    


    let container = document.getElementById("container");

        container.innerHTML = 
        `
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <div class="card shadow-lg">
                        <img src="../images/${result.productImage}" class="card-img-top" alt="Card image">
                        <div class="card-body text-center">
                            <h3  class="card-title">${result.productId}</h3>
                            <h5 class="card-title">${result.productName}</h5>
                            <h5 class="card-title">The Price : ${result.price}</h5>
                            <p class="card-text">${result.description}</p>                         
                        </div>
                    </div>
                </div>
            </div>
        </div>
        `
       
    // })

}

function reset(){
    localStorage.clear();
} 

//  <a class="btn btn-primary" href="details.html" onclick="store(${element.categoryId})">Store Data</a>
// function store(id){
//     localStorage.setItem("CProductID",id);
// }
getCategory();
