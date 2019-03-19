mergeInto(LibraryManager.library, {

  Speak: function (message, vol, voice, pitch, rate) {
    var msg = new SpeechSynthesisUtterance();

	msg.text = Pointer_stringify(message);
    msg.volume = vol;
    msg.voice = speechSynthesis.getVoices()[voice];
    msg.pitch = pitch;
    msg.rate = rate;
	
    window.speechSynthesis.speak(msg);
  },

  CheckSpeechSynthesis: function () {
    if ('speechSynthesis' in window) {
	  return 1;
    } else {
	  return 0;
    }
  },
  
  GetVoicesLength: function () {
    var voices = speechSynthesis.getVoices();
    return voices.length;
  },
  
  GetVoiceName: function (id) {
    var voices = speechSynthesis.getVoices();
	
    var returnStr = voices[id].name;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);

    return buffer;
  },

  ShowMessage: function (str) {
    window.alert(Pointer_stringify(str));
  },

});
