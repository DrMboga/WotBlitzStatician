if (typeof wn7Grade !== 'undefined') {
	ChangeWn7Style(wn7Grade);
}
if (typeof winrateGrade !== 'undefined') {
	ChangeWinrateStyle(winrateGrade);
}

function ChangeWn7Style(grade) {
	var x = document.getElementById("wn7");
	if (x == null) {
		return;
	}
	x.style.color = GetColorByGrade(grade);
}

function ChangeWinrateStyle(grade) {
	var x = document.getElementById("winrate");
	if (x == null) {
		return;
	}
	x.style.color = GetColorByGrade(grade);
}

function GetColorByGrade(grade) {
	switch (grade) {
		case "VeryBad":
			return "#000000";
		case "Bad":
			return "#cd3333";
		case "BelowAverage":
			return "#d77900";
		case "Average":
			return "#d7b600";
		case "Good":
			return "#6d9521";
		case "VeryGood":
			return "#4c762e";
		case "Great":
			return "#4a92b7";
		case "Unicum":
			return "#83579d";
		case "SuperUnicum":
			return "#5a3175";
		default:
			return "#000000";
	}
}
