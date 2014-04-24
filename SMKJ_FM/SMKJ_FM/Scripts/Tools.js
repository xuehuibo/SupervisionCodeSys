var Tools = $.extend({}, Tools);

/*将form表单内的元素序列化为对象，扩展Jquery的一个方法*/
Tools.serializeObject = function (form) { 
    var o = {};
    $.each(form.serializeArray(), function (index) {
        if (o[this['name']]) {
            o[this['name']] = o[this['name']] + "," + this['value'];
        } else {
            o[this['name']] = this['value'];
        }
    });
    return o;
};


/*json对象转string*/
Tools.ObjectToString=function (O) {
        var S = [];
        var J = "";
        if (Object.prototype.toString.apply(O) === '[object Array]') {
            for (var i = 0; i < O.length; i++)
                S.push(Tools.ObjectToString(O[i]));
            J = '[' + S.join(',') + ']';
        }
        else if (Object.prototype.toString.apply(O) === '[object Date]') {
            J = "new Date(" + O.getTime() + ")";
        }
        else if (Object.prototype.toString.apply(O) === '[object RegExp]' || Object.prototype.toString.apply(O) === '[object Function]') {
            J = O.toString();
        }
        else if (Object.prototype.toString.apply(O) === '[object Object]') {
            for (var i in O) {
                O[i] = typeof (O[i]) == 'string' ?  O[i]  : (typeof (O[i]) === 'object' ? Tools.ObjectToString(O[i]) : O[i]);
                S.push('"'+i+'"' + ':\"' + O[i]+"\"");
            }
            J = '{' + S.join(',') + '}';
        }

    return J;
};

/*命名空间 
register 方法注册命名空间*/

Tools.Namespace = new Object();
Tools.Namespace.register = function (fullNS) {
    // 将命名空间切成N部分, 比如Grandsoft、GEA等
    var nsArray = fullNS.split('.');
    var sEval = "";
    var sNS = "";
    for (var i = 0; i < nsArray.length; i++) {
        if (i != 0) sNS += ".";
        sNS += nsArray[i];
        // 依次创建构造命名空间对象（假如不存在的话）的语句
        // 比如先创建Grandsoft，然后创建Grandsoft.GEA，依次下去
        sEval += "if (typeof(" + sNS + ") == 'undefined') " + sNS + " = new Object();"
    }
    if (sEval != "") eval(sEval);
};

/**
* 扩展方法 使datagrid的列中能显示row中的对象里的属性
* 无需调用自动执行 Field：Staff.JoinDate
**/
$.fn.datagrid.defaults.view = $.extend({}, $.fn.datagrid.defaults.view, {
    renderRow: function (target, fields, frozen, rowIndex, rowData) {
        var opts = $.data(target, 'datagrid').options;
        var cc = [];
        if (frozen && opts.rownumbers) {
            var rownumber = rowIndex + 1;
            if (opts.pagination) {
                rownumber += (opts.pageNumber - 1) * opts.pageSize;
            }
            cc.push('<td class="datagrid-td-rownumber"><div class="datagrid-cell-rownumber">' + rownumber + '</div></td>');
        }
        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];
            var col = $(target).datagrid('getColumnOption', field);
            var fieldSp = field.split(".");
            var dta = rowData[fieldSp[0]];
            for (var j = 1; j < fieldSp.length; j++) {
                dta = dta[fieldSp[j]];
            }
            if (col) {
                // get the cell style attribute
                var styleValue = col.styler ? (col.styler(dta, rowData, rowIndex) || '') : '';
                var style = col.hidden ? 'style="display:none;' + styleValue + '"' : (styleValue ? 'style="' + styleValue + '"' : '');

                cc.push('<td field="' + field + '" ' + style + '>');

                var style = 'width:' + (col.boxWidth) + 'px;';
                style += 'text-align:' + (col.align || 'left') + ';';
                style += opts.nowrap == false ? 'white-space:normal;' : '';

                cc.push('<div style="' + style + '" ');
                if (col.checkbox) {
                    cc.push('class="datagrid-cell-check ');
                } else {
                    cc.push('class="datagrid-cell ');
                }
                cc.push('">');

                if (col.checkbox) {
                    cc.push('<input type="checkbox"/>');
                } else if (col.formatter) {
                    cc.push(col.formatter(dta, rowData, rowIndex));
                } else {
                    cc.push(dta);
                }

                cc.push('</div>');
                cc.push('</td>');
            }
        }
        return cc.join('');
    }
});

