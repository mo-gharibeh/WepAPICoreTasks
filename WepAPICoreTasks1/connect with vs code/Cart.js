const url = "https://localhost:44367/api/CartItem"
debugger
async function getCartItems() {

    let response = await fetch(url);
    var result = await response.json();

    console.log(response);

    var container = document.getElementById("container");

    result.forEach(element =>{
        container.innerHTML += 
        `
        <tr>
            <td>${element.cartId}</td>
            <td>${element.product.productName}</td>
            <td><input type="number" name="quantity" id="quantity" value="${element.quantity}" ></input></td>
            <td><a class="btn btn-primary"  onclick="store(${element.cartItemId})">Edit</a></td>
        </tr>
        <!-- Add more rows as needed -->
                    
        `
       
    })

}
function store(id){
    localStorage.setItem("cartItemId",id);
}

function reset(){
    localStorage.clear();
}

getCartItems();