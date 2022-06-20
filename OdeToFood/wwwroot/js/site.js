$(function () {

    let ajaxFormSubmit = function () {
        const $form = $(this);
        const options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        }
        $.ajax(options).done(function (data) {
            const $target = $($form.attr("data-otf-target"));
            const $newHtml = $(data);
            $target.replaceWith($newHtml);
            $newHtml.effect("highlight");
        });
        return false;

    };
    const submitAutocompleteForm = function (event, ui) {
        const $input = $(this);
        $input.val(ui.item.label);
        const $form = $input.parents("form:first");
        $form.submit();
    };
    const createAutocomplete = function () {
        const $input = $(this);
        const options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm
        };
        $input.autocomplete(options);
    };

    $("form [data-otf-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-otf-autocomplete]").each(createAutocomplete);


});