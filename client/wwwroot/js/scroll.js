

function toggleScrollToTopButton() {
    window.addEventListener('scroll', toggleScrollToTopButton);
    var scrollToTopButton = document.getElementById('scrollToTopButton');
    if (window.scrollY > 100) {
        scrollToTopButton.style.visibility = 'visible';
    } else {
        scrollToTopButton.style.visibility = 'hidden';
    }
}

function clearScrollBtn(){
    window.removeEventListener('scroll', toggleScrollToTopButton);
}

function scrollUpSmooth(){
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
      })
}

function scrollUpInstant(){
    window.scrollTo({
        top: 0,
        behavior: 'instant'
      });
}

function addElementClass(id, className) {
    var element = document.getElementById(id);
    element.classList.add(className);
}

function removeElementClass(id, className) {
    var element = document.getElementById(id);
    element.classList.remove(className);
}



