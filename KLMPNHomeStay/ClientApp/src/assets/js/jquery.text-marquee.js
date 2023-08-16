;

(function (factory) {
  "use strict";

  if (typeof define === 'function' && define.amd) {
    define(['jquery'], factory);
  } else if (typeof exports !== 'undefined') {
    module.exports = factory(require('jquery'), window, document);
  } else {
    factory(jQuery, window, document);
  }
})(function ($, window, document, undefined) {
  "use strict"; // undefined is used here as the undefined global variable in ECMAScript 3 is
  // mutable (ie. it can be changed by someone else). undefined isn't really being
  // passed in so we can ensure the value of it is truly undefined. In ES5, undefined
  // can no longer be modified.
  // window and document are passed through as local variables rather than global
  // as this (slightly) quickens the resolution process and can be more efficiently
  // minified (especially when both are regularly referenced in your plugin).

  /**
   * jQuery custom plugin implement the text marquee functionality
   */

  $.fn.textMarquee = function (opts) {
    var defaults = {
      mode: 'bounce' // Available modes: 'bounce', 'loop'

    };
    var settings = $.extend({}, defaults, opts);
    return this.each(function () {
      var _this = this;

      var $this = $(this);
      var htmlContent = $this.html();
      var textContent = $this.text();
      var wrapperWidth = null;
      var contentWidth = null;
      var $wrapper = null;
      var $content = null;
      var timeout = null;

      var destroy = function () {
        $this.html(htmlContent);

        if (timeout) {
          clearTimeout(timeout);
          timeout = null;
        }
      };

      var build = function () {
        destroy();
        /**
         * Remove content
         */

        $this.html('');
        /**
         * Create plugin structure
         */

        $('<span class="text-marquee">').appendTo($this);
        $wrapper = $this.find('.text-marquee');
        $('<span class="text-marquee__text">').html(textContent).appendTo($wrapper);
        $content = $this.find('.text-marquee__text');
        /**
         * Get some values
         */

        wrapperWidth = $wrapper.width();
        contentWidth = $content.width();
        /**
         * Start animation
         */

        start();
        /**
         * Set initialization class
         */

        $wrapper.addClass('text-marquee--initialized');
      };

      var start = function () {
        var startAnimationBounce = function () {
          if (contentWidth > wrapperWidth) {
            var speed = contentWidth * 10;
            $wrapper.animate({
              scrollLeft: contentWidth - wrapperWidth
            }, speed, function () {
              $wrapper.animate({
                scrollLeft: 0
              }, speed, function () {
                startAnimationBounce();
              });
            });
          }
        };

        var indent = wrapperWidth;

        var startAnimationLoop = function () {
          indent--;
          $wrapper.css('text-indent', indent);

          if (indent < -1 * $content.width()) {
            indent = $wrapper.width();
          }

          timeout = setTimeout(startAnimationLoop, 1000 / 60);
        };

        switch (settings.mode) {
          case 'bounce':
          default:
            startAnimationBounce();
            break;

          case 'loop':
            startAnimationLoop();
            break;
        }
      };
      /**
       * Build roadmap
       */


      build();
      $(window).on('resize', function () {
        build();
      });
    });
  };
});