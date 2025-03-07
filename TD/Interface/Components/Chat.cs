﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TD.Interface.Components;
using TD.Common;

using SharpDX.Direct2D1;
using SharpDX;
using SharpDX.Samples;
using SharpDX.DirectWrite;
using System.Windows.Forms;

namespace TD.Interface.Components
{
    class Chat : CommonComponent
    {
        protected List<Tuple<string, string>> _messages;
        
        // Ввод сообщения
        protected bool _isEnterMessage;
        protected string _enterMessage;
     
        public Chat(RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory Factory_, SolidColorBrush Brush, Vector2 position, Size2F size) :
            base(RenderTarget2D, Factory_, Brush, "chat01.png", position, size)
        {
            _messages = new List<Tuple<string, string>>();
            
            _margin = new Vector4(-5, 0, 10, 5);

            _isEnterMessage = false;
            _enterMessage = "";
        }

        public override void Draw(DemoTime time)
        {
            if (!_visible)
                return;

            base.Draw(time);

            // Максимальная ширина блока для текста с учетом отступов
            float width = _size.Width - _margin.Y - _margin.W;

            int step = 0;
            if (_isEnterMessage)
            {
                var message = "Сообщение: " + _enterMessage;
                step += lineInMessage(message, width);
                RenderTarget2D.DrawText(message, _textFormat, new RectangleF(
                    _position.X - _size.Width / 2 + _margin.W,
                    _position.Y + _size.Height / 2 - _lineHeight * step - _margin.Z,
                    width,
                    _lineHeight), _brush);

            }

            // Если включен ввод текста, шаг уже есть
            for (int i = _messages.Count - 1; i >= 0; i--)
            {
                var message = _messages[i].Item1 + ": " + _messages[i].Item2;
                var lineCount = lineInMessage(message, width);

                // Условие выхода. Если весь текст не влазиет, то и не показываем
                // TODO Сделать чтобы показывалась та часть. которая еще влазиет
                if ((step + lineCount) * _lineHeight + _margin.X + _margin.Z > _size.Height)
                    break;

                var outRectangle = new RectangleF(_position.X - _size.Width / 2 + _margin.W,
                                                  _position.Y - _size.Height / 2 - _margin.Z + _size.Height - _lineHeight * (step + lineCount), 
                                                  width,
                                                  _lineHeight * lineCount);
                
                RenderTarget2D.DrawText(message, _textFormat, outRectangle, _brush);
                
                step += lineCount;
            }

        }

        private int lineInMessage(string message, float width)
        {
            var kof = 5.642857142857139; // Коэфциент для 16 кегля
            return (int)((message.Count() * kof) / width + 1);
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);
            // TODO Сделать очищении истории сообщений
        }

        public void Show(bool enterText)
        {
            _visible = true;
            _isEnterMessage = true;
        }

        public void AddMessage(string playerName, string messageText)
        {
            _messages.Add(new Tuple<string, string>(playerName, messageText));
        }

        public override void KeyDown(KeyEventArgs e)
        {
            base.KeyDown(e);

            if (e.KeyCode == Keys.Enter)
            {
                if (_isEnterMessage)
                {
                    _enterMessage = _enterMessage.Trim();

                    if (_enterMessage != "")
                    {
                        _messages.Add(new Tuple<string, string>("Я", _enterMessage));
                        _enterMessage = "";
                    }
                    _isEnterMessage = false;
                    return;
                }
                else
                {
                    _isEnterMessage = true;
                }
            }

            if (!_isEnterMessage)
                return;
            
            if (e.KeyValue >= 'a' && e.KeyValue < 'z'
                || e.KeyValue >= 'A' && e.KeyValue < 'Z')
            {
                var text = e.KeyData.ToString();
                if (!e.Shift)
                    text = text.ToLower();
                else
                    text = text.ToUpper();
                _enterMessage += text[0];
            }

            switch (e.KeyCode)
            {
                // Проверим на цифры
                case Keys.D1:
                    if (e.Shift)
                        _enterMessage += "!";
                    else _enterMessage += "1";
                    break;
                case Keys.D2:
                    if (e.Shift)
                        _enterMessage += "@";
                    else _enterMessage += "2";
                    break;
                case Keys.D3:
                    if (e.Shift)
                        _enterMessage += "#";
                    else _enterMessage += "3";
                    break;
                case Keys.D4:
                    if (e.Shift)
                        _enterMessage += "$";
                    else _enterMessage += "4";
                    break;
                case Keys.D5:
                    if (e.Shift)
                        _enterMessage += "%";
                    else _enterMessage += "5";
                    break;
                case Keys.D6:
                    if (e.Shift)
                        _enterMessage += "^";
                    else _enterMessage += "6";
                    break;
                case Keys.D7:
                    if (e.Shift)
                        _enterMessage += "&";
                    else _enterMessage += "7";
                    break;
                case Keys.D8:
                    if (e.Shift)
                        _enterMessage += "*";
                    else _enterMessage += "8";
                    break;
                case Keys.D9:
                    if (e.Shift)
                        _enterMessage += "(";
                    else _enterMessage += "9";
                    break;
                case Keys.D0:
                    if (e.Shift)
                        _enterMessage += ")";
                    else _enterMessage += "0";
                    break;
                // Проверим на особые кнопки
                case Keys.OemSemicolon:
                    if (e.Shift)
                        _enterMessage += ":";
                    else _enterMessage += ";";
                    break;
                case Keys.OemQuotes:
                    if (e.Shift)
                        _enterMessage += "\"";
                    else _enterMessage += "'";
                    break;
                case Keys.OemQuestion:
                    if (e.Shift)
                        _enterMessage += "?";
                    else _enterMessage += "/";
                    break;
                case Keys.OemPipe:
                    if (e.Shift)
                        _enterMessage += "|";
                    else _enterMessage += "\\";
                    break;
                case Keys.OemPeriod:
                    if (e.Shift)
                        _enterMessage += ">";
                    else _enterMessage += ".";
                    break;
                case Keys.Oemcomma:
                    if (e.Shift)
                        _enterMessage += "<";
                    else _enterMessage += ",";
                    break;
                case Keys.OemOpenBrackets:
                    if (e.Shift)
                        _enterMessage += "{";
                    else _enterMessage += "[";
                    break;
                case Keys.OemCloseBrackets:
                    if (e.Shift)
                        _enterMessage += "}";
                    else _enterMessage += "]";
                    break;
                case Keys.OemMinus:
                    if (e.Shift)
                        _enterMessage += "_";
                    else _enterMessage += "-";
                    break;
                case Keys.Oemplus:
                    if (e.Shift)
                        _enterMessage += "+";
                    else _enterMessage += "=";
                    break;
            }

            if (e.KeyCode == Keys.Space)
                _enterMessage += " ";

            if (e.KeyCode == Keys.Back)
                if (_enterMessage.Count() < 1)
                    _enterMessage = "";
                else _enterMessage = _enterMessage.Remove(_enterMessage.Count() - 1);
        }
    }
}
