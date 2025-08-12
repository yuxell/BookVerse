
// Modal functionality
const loginBtn = document.getElementById('loginBtn');
const loginModal = document.getElementById('loginModal');
const closeModal = document.getElementById('closeModal');
const loginForm = document.getElementById('loginForm');

// Open modal
function openModal() {
    loginModal.classList.add('active');
    document.body.style.overflow = 'hidden';
}

// Close modal
const closeLoginModal = () => {
  loginModal.classList.remove('active');
  document.body.style.overflow = '';
};

closeModal.addEventListener('click', closeLoginModal);

// Close modal when clicking outside
loginModal.addEventListener('click', (e) => {
  if (e.target === loginModal) {
    closeLoginModal();
  }
});

