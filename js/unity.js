var fullscreen = false
var canvas = document.querySelector("#unity-canvas");
var loadingBar = document.querySelector("#unity-loading-bar");
var progressBarFull = document.querySelector("#unity-progress-bar-full");
var fullscreenButton = document.querySelector("#unity-fullscreen-button");
var warningBanner = document.querySelector("#unity-warning");

function unityShowBanner(msg, type) {
    var div = document.createElement('div');
    div.innerHTML = msg;
    warningBanner.appendChild(div);
    if (type == 'error') {
        console.error(msg)
    } else if (type == 'warning') {
        console.warn(msg)
    }
          
}

        var buildUrl = "unity";
        var loaderUrl = buildUrl + "/WebGL.loader.js";
        var config = {
        dataUrl: buildUrl + "/WebGL.data",
        frameworkUrl: buildUrl + "/WebGL.framework.js",
        codeUrl: buildUrl + "/WebGL.wasm",
        streamingAssetsUrl: "StreamingAssets",
        companyName: "DefaultCompany",
        productName: "Sports",
        productVersion: "1.0",
        showBanner: unityShowBanner,
        };

        canvas.addEventListener("fullscreenchange", () => {
            fullscreen = !fullscreen
            if (fullscreen) {
                canvas.style.width = "960px"
                canvas.style.height = "540px"
            } else {
                canvas.style.width = "100%"
                canvas.style.height = "auto"
            }
        })

        loadingBar.style.display = "block";

        var script = document.createElement("script");
        script.src = loaderUrl;
        script.onload = () => {
        createUnityInstance(canvas, config, (progress) => {
            progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
            loadingBar.style.display = "none";
            fullscreenButton.onclick = () => {
            unityInstance.SetFullscreen(1);
            };
        }).catch((message) => {
            alert(message);
        });
        };


        document.body.appendChild(script);