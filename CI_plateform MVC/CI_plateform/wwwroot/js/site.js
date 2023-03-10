// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function compareCity(selectedCities, cardCity) {
    let isMatch = selectedCities.some(function (selectedCity) {
        return cardCity == selectedCity;
    })
    return isMatch;
}

function compareTheme(selectedThemes, cardTheme) {
    let isMatch = selectedThemes.some(function (selectedTheme) {
        return cardTheme == selectedTheme;
    })
    return isMatch;
}

function compareSkill(selectedSkills, cardSkills) {
    let isMatch = selectedSkills.some(function (selectedSkill) {
        return cardSkills.some(function (cardSkill) {
            return cardSkill == selectedSkill;
        })
    })
    return isMatch;
}

var allDropdowns = $('.dropdown ul');

const Filter = () => {

    var selectedCities = $('.City-Dropdown :checkbox:checked').map(function () { return $(this).val(); }).get();

    var selectedThemes = $('.Theme-Dropdown :checkbox:checked').map(function () { return $(this).val(); }).get();

    var selectedSkills = $('.Skill-Dropdown :checkbox:checked').map(function () { return $(this).val(); }).get();



    let atleastOneCity = (selectedCities.length != 0);
    let noCity = (selectedCities.length == 0);

    let atleastOneTheme = (selectedThemes.length != 0);
    let noTheme = (selectedThemes.length == 0);

    let atleastOneSkill = (selectedSkills.length != 0);
    let noSkill = (selectedSkills.length == 0);


    if (noCity && noTheme && noSkill && selectedCountry == null) {
        // If no themes are selected, show all cards
        console.log("no themes and skills selected");
        $('.search-mission-card').show();
    }
    else {
        $('.search-mission-card').each(function () {
            var cardCountry = $(this).find('.Card-CountryId').val();
            let cardCity = $(this).find('.Card-CityId').val();
            let cardTheme = $(this).find('.Card-ThemeId').val();
            let cardSkills = $(this).find('.Card-SkillId').map(function () { return $(this).val(); }).get();

            if (selectedCountry != null) {
                console.log(cardCountry);

                if (atleastOneCity) {
                    if (atleastOneTheme) {
                        if (atleastOneSkill) {
                            let isMatch = (selectedCountry == cardCountry) && compareCity(selectedCities, cardCity) && compareTheme(selectedThemes, cardTheme) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                        else {
                            let isMatch = (selectedCountry == cardCountry) && compareCity(selectedCities, cardCity) && compareTheme(selectedThemes, cardTheme);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                    }
                    else {
                        if (atleastOneSkill) {
                            let isMatch = (selectedCountry == cardCountry) && compareCity(selectedCities, cardCity) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                        else {
                            let isMatch = (selectedCountry == cardCountry) && compareCity(selectedCities, cardCity);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                    }
                }
                else {
                    if (atleastOneTheme) {
                        if (atleastOneSkill) {
                            let isMatch = (selectedCountry == cardCountry) && compareTheme(selectedThemes, cardTheme) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                        else {
                            let isMatch = (selectedCountry == cardCountry) && compareTheme(selectedThemes, cardTheme);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                    }
                    else {
                        if (atleastOneSkill) {
                            let isMatch = (selectedCountry == cardCountry) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                        else {
                            let isMatch = (selectedCountry == cardCountry);
                            (isMatch) ? $(this).show() : $(this).hide();

                        }
                    }
                }
            }
            else {
                if (atleastOneCity) {
                    if (atleastOneTheme) {
                        if (atleastOneSkill) {
                            let isMatch = compareCity(selectedCities, cardCity) && compareTheme(selectedThemes, cardTheme) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).find('.search-mission-card').show() : $(this).find('.search-mission-card').hide();
                        }
                        else {
                            let isMatch = compareCity(selectedCities, cardCity) && compareTheme(selectedThemes, cardTheme);
                            (isMatch) ? $(this).find('.search-mission-card').show() : $(this).find('.search-mission-card').hide();
                        }
                    }
                    else {
                        if (atleastOneSkill) {
                            let isMatch = compareCity(selectedCities, cardCity) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).find('.search-mission-card').show() : $(this).find('.search-mission-card').hide();
                        }
                        else {
                            let isMatch = compareCity(selectedCities, cardCity);
                            (isMatch) ? $(this).find('.search-mission-card').show() : $(this).find('.search-mission-card').hide();
                        }
                    }
                }
                else {
                    if (atleastOneTheme) {
                        if (atleastOneSkill) {
                            let isMatch = compareTheme(selectedThemes, cardTheme) && compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                        else {

                            let isMatch = compareTheme(selectedThemes, cardTheme);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                    }
                    else {
                        if (atleastOneSkill) {
                            let isMatch = compareSkill(selectedSkills, cardSkills);
                            (isMatch) ? $(this).show() : $(this).hide();
                        }
                        else {
                            $(this).show();
                        }
                    }
                }
            }

        });


    }
}

allDropdowns.each(function () {
    let dropdown = $(this);
    $(this).on('change', 'input[type="checkbox"]', function () {
        Filter();
    });
});

