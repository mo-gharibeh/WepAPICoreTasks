const n = localStorage.getItem("ProductID");

const url = `https://localhost:44367/api/Products/Product/${n}`;
console.log(url);

async function getCategory() {

    let response = await fetch(url);
    let result = await response.json();
    console.log(result[0]);
    


    let container = document.getElementById("container");

        container.innerHTML = 
        `
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <div class="card shadow-lg">
                        <img src="../images/${result[0].productImage}" class="card-img-top" alt="Card image">
                        <div class="card-body text-center">
                            <h3  class="card-title">${result[0].productId}</h3>
                            <h5 class="card-title">${result[0].productName}</h5>
                            <h5 class="card-title">The Price : ${result[0].price}</h5>
                            <p class="card-text">${result[0].description}</p>                         
                        </div>
                        <input type="number" name="quantity" id="quantity" ></input>
                        <button type="button" class="btn btn-primary mt-4 w-100" onclick="addToCart()">Add To Cart</button>
                        <a href="editProduct.html" class="btn btn-primary mt-4 w-100">Edit Product</a>
                    </div>
                </div>
            </div>
        </div>
        `
       
    // })

}


async function addToCart(){
    let quantity = document.getElementById("quantity").value;
    const url1 = "https://localhost:44367/api/CartItem"
    var request = {
        productId: n,
        quantity: quantity,
        cartId : 4
    }
    let response = await fetch (url1, 
        {
            method: 'POST',
            body : JSON.stringify(request),
            headers : {
                'Content-Type' : 'application/json'
            }
        }
    )
    

    alert("Product added to cart");
    
}
function reset(){
    localStorage.clear();
} 

//  <a class="btn btn-primary" href="details.html" onclick="store(${element.categoryId})">Store Data</a>
// function store(id){
//     localStorage.setItem("CProductID",id);
// }
getCategory();
