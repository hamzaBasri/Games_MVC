$(document).ready(function () {
    let debounceTimer;
    const $input = $('#searchNameInput');
    const $box = $('#suggestionsBox');

    $input.on('input', function () {
        clearTimeout(debounceTimer);
        const term = $input.val();

        if (term.length < 2) {
            $box.empty();
            return;
        }

        debounceTimer = setTimeout(function () {
            $.get('/Customer/Home/SearchTitles', { term: term }, function (data) {
                $box.empty();
                data.forEach(function (title) {
                    $box.append(`<button type="button" class="list-group-item list-group-item-action">${title}</button>`);
                });
            });
        }, 300);
    });

    $box.on('click', 'button', function () {
        $input.val($(this).text());
        $box.empty();
        $input.closest('form').submit();
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest('#searchNameInput, #suggestionsBox').length) {
            $box.empty();
        }
    });
});