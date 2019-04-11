$(function () {
    var iconButtons = $(".icon-buttons");
    var iconContainer = $(".account-logged-in-btns");
    var iconContainerWidth = +iconContainer.width();

    iconButtons.css("right", iconContainerWidth + 4);

    $(".user-icon").click(function (event) {
        event.stopPropagation();

        if (iconButtons.is(':hidden')) {
            iconButtons.css("display", "flex").hide().fadeIn();
        } else {
            iconButtons.fadeOut();
        }
    });

    $(window).click(function () {
        if (iconButtons.is(':visible')) {
            iconButtons.fadeOut();
        }
    });

    $(".switch").click(function () {
        var checkbox = $(this).children('input[type="checkbox"]');
        var label = $("#Javan");

        if (checkbox.is(':checked')) {
            label.html('Privatan');
        } else {
            label.html('Javan');
        }
    });

    var slikaUrl = $("#SlikaURL");
    var addedImage = $("#recept-added-image");
    var inputImage = $("#recept-input-image");
    var reader = new FileReader();

    reader.onload = function (e) {
        addedImage = $("#recept-added-image");

        if (addedImage.length) {
            addedImage.attr('src', e.target.result);
        } else {
            inputImage.after('<img src="' + e.target.result + '" id="recept-added-image" />');
        }
    }

    if (slikaUrl && slikaUrl.val()) {
        var url = '/slike-recepata/' + slikaUrl.val();
        inputImage.after('<img src="' + url + '" id="recept-added-image" />');
    }

    inputImage.change(function () {
        var input = this;

        if (input.files && input.files[0]) {
            slikaUrl.val('');
            reader.readAsDataURL(input.files[0]);
        }
    });

    var kategorija = null;

    $("#select-filter-kategorije").change(function () {
        kategorija = $(this).val();
        getReceptiData('/Recepti/FilterPoKategoriji');
    });

    $("#btn-search-recepti").click(function () {
        getReceptiData('/Recepti/Search');
    });

    function getReceptiData(url) {
        var inputSearchVal = $("#search-recepti").val();
        var btnDodajRecept = $(".btn-dodaj-recept");
        var loader = $(".loader");
        var receptiList = $(".recepti-list");

        loader.show();
        receptiList.remove();

        $.ajax({
            url: url,
            data: { keyword: inputSearchVal, isHomePage: btnDodajRecept.length > 0, kategorija: kategorija },
            success: function (data) {
                loader.hide();
                $(".recepti-container").append(data);
            }
        });
    }

    $(".form-ban").submit(function () {
        if (confirm('Da li zaista želite ban/unban korisnika?')) {
            return true;
        }

        return false;
    });

    $(".form-brisi").submit(function () {
        if (confirm('Da li zaista želite izbrisati ovog korisnika?')) {
            return true;
        }

        return false;
    });

    $(".form-recept-brisi").submit(function () {
        if (confirm('Da li zaista želite izbrisati ovaj recept?')) {
            return true;
        }

        return false;
    });

    setTimeout(function () {
        $(".notification-container").fadeOut(400, function () { $(this).remove(); });
    }, 3000);
});