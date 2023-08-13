window.addEventListener('scroll', function () {
	var button = document.getElementById('btnFloat');
	var scrollPosition = window.pageYOffset;

	if (scrollPosition > 0) {
		button.classList.add('fade-in');
		button.classList.remove('fade-out');
	} else {
		button.classList.add('fade-out');
		button.classList.remove('fade-in');
	}
});