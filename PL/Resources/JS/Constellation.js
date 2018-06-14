var mousePosition = {};
mousePosition.x = 0;
mousePosition.y = 0;

$(window).mousemove(function (event) {
    mousePosition.x = event.pageX;
    mousePosition.y = event.pageY;
});

function Constellation(canvas) {
    var _this = this,
        context = canvas.getContext('2d'),
        config = {
            star: {
                color: '#2e2d3e',
                width: 2
            },
            line: {
                color: '#2e2d3e',
                width: 0.8
            },
            left: () => { return config.canvasposition.left; },
            right: () => { return config.canvasposition.left + config.width; },
            top: () => { return config.canvasposition.top; },
            bottom: () => { return config.canvasposition.top + config.height; },
            position: {
                x: () => { return mousePosition.x - config.left(); },
                y: () => { return mousePosition.y - config.top(); },
            },
            width: window.innerWidth,
            height: window.innerHeight,
            canvasposition: {
                top: 0,
                left: 0
            },
            flag: false,
            length: (canvas.width * canvas.height) / 1250,
            distance: 25,
            radius: 50,
            stars: []
        };

    this.addconfig = function (Config) {
        console.log(Config);
        config.star = (Config.star) ? Config.star : config.star;
        config.line = (Config.line) ? Config.line : config.line;
        config.width = (Config.width) ? Config.width : config.width;
        config.height = (Config.height) ? Config.height : config.height;
        config.length = (Config.length) ? Config.length : config.length;
        config.distance = (Config.distance) ? Config.distance : config.distance;
        config.radius = (Config.radius) ? Config.radius : config.radius;
        config.canvasposition = (Config.canvasposition) ? Config.canvasposition : config.canvasposition;
        config.flag = (Config.flag) ? Config.flag : config.flag;
    }

    function Star() {
        this.x = Math.random() * canvas.width;
        this.y = Math.random() * canvas.height;
        this.radius = Math.random() * config.star.width;

        this.vx = (0.5 - Math.random()) / 1.2;
        this.vy = (0.5 - Math.random()) / 1.2;

        this.create = function () {
            context.beginPath();
            context.arc(this.x, this.y, this.radius, 0, Math.PI * 2, false);
            context.fill();
        };
    }

    this.animate = function () {
        var i;
        for (i = 0; i < config.length; i++) {

            var star = config.stars[i];

            if (star.y < 0 || star.y > canvas.height) {
                star.vy = - star.vy;
            } else if (star.x < 0 || star.x > canvas.width) {
                star.vx = - star.vx;
            }

            star.x += star.vx;
            star.y += star.vy;
        }
    };

    this.line = function () {
        var length = config.length,
            iStar,
            jStar,
            i,
            j;

        for (i = 0; i < length; i++) {
            iStar = config.stars[i];
            context.beginPath();
            var mouseInCanvase = (
                Math.abs(iStar.x - config.position.x()) < config.radius && Math.abs(iStar.y - config.position.y()) < config.radius
                && config.position.x() > 0 && config.position.x() < config.width
                && config.position.y() > 0 && config.position.y() < config.height
            );

            for (j = i + 1; j < length; j++) {
                jStar = config.stars[j];

                if (Math.abs(iStar.x - jStar.x) < config.distance && Math.abs(iStar.y - jStar.y) < config.distance) {
                    context.moveTo(iStar.x, iStar.y);
                    context.lineTo(jStar.x, jStar.y);
                }
                else if (
                    mouseInCanvase &&
                    Math.abs(iStar.x - config.position.x()) < config.radius && Math.abs(iStar.y - config.position.y()) < config.radius
                    && Math.abs(iStar.x - jStar.x) < config.distance * 2 && Math.abs(iStar.y - jStar.y) < config.distance * 2
                ) {
                    context.moveTo(iStar.x, iStar.y);
                    context.lineTo(jStar.x, jStar.y);
                }
            }

            if (mouseInCanvase) {
                context.moveTo(iStar.x, iStar.y);
                context.lineTo(config.position.x(), config.position.y());
            }

            context.stroke();
            context.closePath();
        }
    };

    this.CreateStarsArray = function () {
        for (var i = 0; i < config.length; i++) {
            config.stars.push(new Star());
        }
    }

    this.createStars = function () {
        context.clearRect(0, 0, canvas.width, canvas.height);

        for (var i = 0; i < config.length; i++) {
            config.stars[i].create();
        }

        _this.animate();
        _this.line();
    };

    this.setCanvas = function () {
        canvas.width = config.width;
        canvas.height = config.height;
    };

    this.setContext = function () {
        context.fillStyle = config.star.color;
        context.strokeStyle = config.line.color;
        context.lineWidth = config.line.width;
    };

    this.loop = function (callback) {
        callback();

        window.requestAnimationFrame(function () {
            _this.loop(callback);
        });
    };

    this.init = function () {
        this.setCanvas();
        this.setContext();
        this.CreateStarsArray();
        this.loop(this.createStars);
    };
}