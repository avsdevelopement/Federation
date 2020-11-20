span.tooltip {
    position: absolute;
    width: 128px;
    height: 48px;
    line-height: 48px;
    padding: 0px 16px 0px 8px;
    font-size: 22px;
    text-align: center;
    color: rgb(255, 255, 255);
    background: rgb(96, 172, 57);
    border: 0px 0px 0px 0px none none none none rgb(255, 255, 255) rgb(70, 126, 42) rgb(255, 255, 255) rgb(255, 255, 255);
    border-radius: 0px 3.2px 3.2px 0px;
    text-shadow: rgba(0, 0, 0, 0.6) 1px 1px 1px;
    box-shadow: rgb(85, 153, 51) -4px 0px 4px 0px inset;
}

span.tooltip:after {
    content: "";
    position: absolute;
    width: 0;
    height: 0;
    border-width: 24px;
    border-style: solid;
    border-color: transparent #60AC39 transparent transparent;
    top: 0px;
    left: -48px;
}