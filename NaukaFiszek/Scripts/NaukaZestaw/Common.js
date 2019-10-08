var prefixAdress =  '/NaukaZestaw/';
function TeachBagAdress(adress) {
    return prefixAdress + adress;
}
function ShowBagEditor() {
    $('#BagEditor').show();
    $('#NewBagLink').hide();
}
class Bag {

    constructor(periodTime, isLimitTime, limitTimeInSek, id, typeAnswear) {

        this.PeriodTime = periodTime;
        this.IsLimitTime = isLimitTime;
        this.LimitTimeInSek = limitTimeInSek;
        this.Id = id;
        this.TypeAnswear = typeAnswear;
    }
}
