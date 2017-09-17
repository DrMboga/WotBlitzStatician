ChangeDeltaStyle();

function ChangeDeltaStyle() {
	var smallElements = document.getElementsByTagName("small");
	for (var i = 0; i < smallElements.length; i++) {
		var text = smallElements[i].innerText;
		if (text.indexOf("+") === 0) {
			smallElements[i].style.color = "Green";
		}
		if (text.indexOf("-") === 0) {
			smallElements[i].style.color = "Red";
		}
	}
}

