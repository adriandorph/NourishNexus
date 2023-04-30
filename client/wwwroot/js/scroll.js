window.addEventListener('scroll', toggleScrollToTopButton);

function toggleScrollToTopButton() {
    var scrollToTopButton = document.getElementById('scrollToTopButton');
    if (window.scrollY > 100) {
        scrollToTopButton.style.visibility = 'visible';
    } else {
        scrollToTopButton.style.visibility = 'hidden';
    }
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



