  	// **************   Lo que puede hacer, para aquellos que quieran usar este script :DDDDDD
 	//formatNumber(3, "$0.00")
	//$3.00
	//formatNumber(3.14159265, "##0.####")
	//3.1416

	//formatNumber(3.14, "0.0###%")
	//314.0%

	//formatNumber(314159, ",##0.####")
	//314,159

	//formatNumber(31415962, "$,##0.00")
	//$31,415,962.00

	//formatNumber(cat43, "0.####%")
    //null
	

	//formatNumber(0.5, "#.00##")
	//0.50

	//formatNumber(0.5, "0.00##")
	//0.50

	//formatNumber(0.5, "00.00##")
	//00.50

	//formatNumber(4.44444, "0.00")
	//4.44

	//formatNumber(5.55555, "0.00")
	//5.56

	//formatNumber(9.99999, "0.00")
	//10.00
	
	// ***************** Esos fueron las muestras primer lugar donde las uso es en IseleñasDemo jdmmrs08 :DDD

  var separator = ",";
  var decpoint = "."; 
  var percent = "%";
  var currency = "$"; 

  function formatNumber(number, format, print) {
    if (print) document.write("formatNumber(" + number + ", \"" + format + "\")<br>");

    if (number - 0 != number) return null;  
    var useSeparator = format.indexOf(separator) != -1; 
    var usePercent = format.indexOf(percent) != -1;  
    var useCurrency = format.indexOf(currency) != -1; 
    var isNegative = (number < 0);
    number = Math.abs (number);
    if (usePercent) number *= 100;
    format = strip(format, separator + percent + currency); 
    number = "" + number;

    var dec = number.indexOf(decpoint) != -1;
    var nleftEnd = (dec) ? number.substring(0, number.indexOf(".")) : number;
    var nrightEnd = (dec) ? number.substring(number.indexOf(".") + 1) : "";

    dec = format.indexOf(decpoint) != -1;
    var sleftEnd = (dec) ? format.substring(0, format.indexOf(".")) : format;
    var srightEnd = (dec) ? format.substring(format.indexOf(".") + 1) : "";

    if (srightEnd.length < nrightEnd.length) {
      var nextChar = nrightEnd.charAt(srightEnd.length) - 0;
      nrightEnd = nrightEnd.substring(0, srightEnd.length);
      if (nextChar >= 5) nrightEnd = "" + ((nrightEnd - 0) + 1);  // round up

      while (srightEnd.length > nrightEnd.length) {
        nrightEnd = "0" + nrightEnd;
      }

      if (srightEnd.length < nrightEnd.length) {
        nrightEnd = nrightEnd.substring(1);
        nleftEnd = (nleftEnd - 0) + 1;
      }
    } else {
      for (var i=nrightEnd.length; srightEnd.length > nrightEnd.length; i++) {
        if (srightEnd.charAt(i) == "0") nrightEnd += "0";
        else break;
      }
    }

    sleftEnd = strip(sleftEnd, "#"); 
    while (sleftEnd.length > nleftEnd.length) {
      nleftEnd = "0" + nleftEnd;
    }

    if (useSeparator) nleftEnd = separate(nleftEnd, separator); 
    var output = nleftEnd + ((nrightEnd != "") ? "." + nrightEnd : "");
    output = ((useCurrency) ? currency : "") + output + ((usePercent) ? percent : "");
    if (isNegative) {
      output = (useCurrency) ? "(" + output + ")" : "-" + output;
    }
    return output;
  }

  function strip(input, chars) {
    var output = "";
    for (var i=0; i < input.length; i++)
      if (chars.indexOf(input.charAt(i)) == -1)
        output += input.charAt(i);
    return output;
  }

  function separate(input, separator) {
    input = "" + input;
    var output = ""; 
    for (var i=0; i < input.length; i++) {
      if (i != 0 && (input.length - i) % 3 == 0) output += separator;
      output += input.charAt(i);
    }
    return output;
  }