$(document).ready(function(){

    loadItem();

    var totalMoney = 0.00;
    var quarterCount = 0;
    var dimeCount = 0;
    var nickelCount = 0;

    $('#add-dollar').click(function(event){
        
        totalMoney += 1.00;
        quarterCount += 4;
        $("#display-change").val(null);
        $("#display-messages").val(null);
        $("#total-money-display").val(totalMoney.toFixed(2));
    });

    $('#add-quarter').click(function(event){
        
        totalMoney += 0.25;
        quarterCount +=1;
        $("#display-change").val(null);
        $("#display-messages").val(null);
        $("#total-money-display").val(totalMoney.toFixed(2));
    });

     $('#add-dime').click(function(event){
        
        totalMoney += 0.10;
        dimeCount += 1;
        $("#display-change").val(null);
        $("#display-messages").val(null);
        $("#total-money-display").val(totalMoney.toFixed(2));
    });

     $('#add-nickel').click(function(event){
        
        totalMoney += 0.05;
        nickelCount += 1;
        $("#display-change").val(null);
        $("#display-messages").val(null);
        $("#total-money-display").val(totalMoney.toFixed(2));
    });

    $("#make-purchase").click(function(){
        var moneyTotal = $("#total-money-display").val();
        var itemId = $("#display-item").val();
        if (itemId == "")
        {
            $("#display-messages").val("You forgot to choose an item");
        }

        else if (moneyTotal == 0)
        {
            $("#display-messages").val("Please insert money");
        }

        else
        {
            $.ajax({
                type: "GET",
                url: "https://llama-vending.herokuapp.com/money/" + moneyTotal + "/item/" + itemId,
                success: function(data, status) {
                    
                    $("#total-money-display").val(null);
                    $("#display-messages").val("THANK YOU");
                    $("#display-change").val(data.quarters + " Quarters, " + data.dimes + " Dimes, " + data.nickels + " Nickels, " + data.pennies + " Pennies");
                    totalMoney = 0;
                    quarterCount = 0;
                    dimeCount = 0;
                    nickelCount = 0;
                    loadItem();
                },
                error: function(item) {
                    $("#display-messages").val(item.responseJSON.message);
                }
            })
            
        }
        


        
        
    })

    $("#change-return").click(function(){
        
        $("display-messages").val(null);
        $("#total-money-display").val(null);
        $("#display-change").val(quarterCount + " Quarters, " + dimeCount + " Dimes, " + nickelCount + " Nickels")
        totalMoney = 0;
        quarterCount = 0;
        dimeCount = 0;
        nickelCount = 0;
    })




})

function loadItem(){
    $("#item").empty();
    $.ajax({
        type: "GET",
        url: "https://llama-vending.herokuapp.com/items",
        success: function(itemArray) {
            var itemDiv = $("#item");
            
            $.each(itemArray, function(index, item){
                
                var itemInfo = "<div class='container-fluid col-sm-4'><div class='well' id='item" + item.id + "'><center>";
                itemInfo += "<a class='btn btn-default' id='button'>";
                itemInfo += "<div align='left'>" + item.id + "</div>";
                itemInfo += item.name + "<br>";
                itemInfo += "$" + item.price.toFixed(2) + "<br>";
                itemInfo += "Quantity Left: " + item.quantity + "</a></center></div></div>";

                itemDiv.append(itemInfo);

                $("#item" + item.id + "").on("click", function(){ 
                    $("#display-messages").val(null);
                    $("#display-change").val(null);
                    $('#display-item').val(item.id);
                })


            });
            
            

        }, 
        error: function() {
            alert("Error loading file.");
        }
    });
}