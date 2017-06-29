$(document).ready(function(){

    loadCandy();

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
        var candyId = $("#display-item").val();
        if (candyId == "")
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
                url: "http://localhost:8080/money/" + moneyTotal + "/item/" + candyId,
                success: function(data, status) {
                    
                    $("#total-money-display").val(null);
                    $("#display-messages").val("THANK YOU");
                    $("#display-change").val(data.quarters + " Quarters, " + data.dimes + " Dimes, " + data.nickels + " Nickels, " + data.pennies + " Pennies");
                    totalMoney = 0;
                    quarterCount = 0;
                    dimeCount = 0;
                    nickelCount = 0;
                    loadCandy();
                },
                error: function(candy) {
                    $("#display-messages").val(candy.responseJSON.message);
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

function loadCandy(){
    $("#candy").empty();
    $.ajax({
        type: "GET",
        url: "http://localhost:8080/items",
        success: function(candyArray) {
            var candyDiv = $("#candy");
            
            $.each(candyArray, function(index, candy){
                
                var candyInfo = "<div class='container-fluid col-sm-4'><div class='well' id='candy" + candy.id + "'><center>";
                candyInfo += "<a class='btn btn-default' id='button'>";
                candyInfo += "<div align='left'>" + candy.id + "</div>";
                candyInfo += candy.name + "<br>";
                candyInfo += "$" + candy.price.toFixed(2) + "<br>";
                candyInfo += "Quantity Left: " + candy.quantity + "</a></center></div></div>";

                candyDiv.append(candyInfo);

                $("#candy" + candy.id + "").on("click", function(){ 
                    $("#display-messages").val(null);
                    $("#display-change").val(null);
                    $('#display-item').val(candy.id);
                })


            });
            
            

        }, 
        error: function() {
            alert("WHERE'S MY CANDY?!");
        }
    });
}