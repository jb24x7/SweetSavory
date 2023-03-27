const pause = async (ms) => new Promise(resolve => setTimeout(resolve, ms));

const swapClass = (element, oldClass, newClass) => {
  if (element.classList.contains(oldClass)) {
    element.classList.remove(oldClass);
  }
  element.classList.add(newClass);
};

window.addEventListener('load', async () => {
  document.documentElement.style.setProperty('--actual-height', window.innerHeight + 'px');
  document.querySelector('main').classList.remove('obscured');
});

function showModal(type) {
  document.body.classList.add('veiled');
  let modalElement = document.getElementById(`${type}-modal`);
  modalElement.classList.remove('obscured');
}

function hideModal(type) {
  document.body.classList.remove('veiled');
  document.getElementById(`${type}-modal`).classList.add('obscured');
}



function formatPhoneNumber(e) { // cannot backspace to delete right parenthesis
  let currentValue = e.target.value;

  // remove any current formatting
  currentValue = currentValue.replaceAll('(', '');
  currentValue = currentValue.replaceAll(')', '');
  currentValue = currentValue.replaceAll('-', '');
  currentValue = currentValue.replaceAll(' ', '');

  // format again
  let newValue = currentValue;
  if (currentValue.length < 3) {
    // add nothing
  } else if (currentValue.length === 3) {
    newValue = `(${currentValue})`;
  } else if (currentValue.length <= 6) {
    newValue = `(${currentValue.substr(0, 3)}) ${currentValue.substr(3, currentValue.length)}`;
  } else {
    newValue = `(${currentValue.substr(0, 3)}) ${currentValue.substr(3, 3)}-${currentValue.substr(6, 4)}`;
  }
  e.target.value = newValue;
}
