namespace Nammedia.Medboss.controls
{
    using DevComponents.DotNetBar;
    using Nammedia.Medboss;
    using Nammedia.Medboss.Favorite;
    using Nammedia.Medboss.Log;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class OperatorBase : MedUIBase, IOperator
    {
        protected Hashtable eventTable = new Hashtable();

        public event ValidateHandler DataInvalid
        {
            add
            {
                this.eventTable["Invalid"] = (ValidateHandler) Delegate.Combine((ValidateHandler) this.eventTable["Invalid"], value);
            }
            remove
            {
                this.eventTable["Invalid"] = (ValidateHandler) Delegate.Remove((ValidateHandler) this.eventTable["Invalid"], value);
            }
        }

        public event DeleteFinishHandler DeleteFinished
        {
            add
            {
                this.eventTable["DeleteFinished"] = (DeleteFinishHandler) Delegate.Combine((DeleteFinishHandler) this.eventTable["DeleteFinished"], value);
            }
            remove
            {
                this.eventTable["DeleteFinished"] = (DeleteFinishHandler) Delegate.Remove((DeleteFinishHandler) this.eventTable["DeleteFinished"], value);
            }
        }

        public event f0_0 DeleteUnfinished
        {
            add
            {
                this.eventTable["DeleteUnfinished"] = (f0_0) Delegate.Combine((f0_0) this.eventTable["DeleteUnfinished"], value);
            }
            remove
            {
                this.eventTable["DeleteUnfinished"] = (f0_0) Delegate.Remove((f0_0) this.eventTable["DeleteUnfinished"], value);
            }
        }

        public event InsertFinishHandler InsertFinished
        {
            add
            {
                this.eventTable["InsertFinished"] = (InsertFinishHandler) Delegate.Combine((InsertFinishHandler) this.eventTable["InsertFinished"], value);
            }
            remove
            {
                this.eventTable["InsertFinished"] = (InsertFinishHandler) Delegate.Remove((InsertFinishHandler) this.eventTable["InsertFinished"], value);
            }
        }

        public event f0_0 InsertUnfinished
        {
            add
            {
                this.eventTable["InsertUnfinished"] = (f0_0) Delegate.Combine((f0_0) this.eventTable["InsertUnfinished"], value);
            }
            remove
            {
                this.eventTable["InsertUnfinished"] = (f0_0) Delegate.Remove((f0_0) this.eventTable["InsertUnfinished"], value);
            }
        }

        public event SaveFunc SaveFired
        {
            add
            {
                this.eventTable["SaveFired"] = (SaveFunc) Delegate.Combine((SaveFunc) this.eventTable["SaveFired"], value);
            }
            remove
            {
                this.eventTable["SaveFired"] = (SaveFunc) Delegate.Remove((SaveFunc) this.eventTable["SaveFired"], value);
            }
        }

        public event UnfoundHandler Unfound
        {
            add
            {
                this.eventTable["Unfound"] = (UnfoundHandler) Delegate.Combine((UnfoundHandler) this.eventTable["Unfound"], value);
            }
            remove
            {
                this.eventTable["Unfound"] = (UnfoundHandler) Delegate.Remove((UnfoundHandler) this.eventTable["Unfound"], value);
            }
        }

        public event UpdateFinishHandler UpdateFinished
        {
            add
            {
                this.eventTable["UpdateFinished"] = (UpdateFinishHandler) Delegate.Combine((UpdateFinishHandler) this.eventTable["UpdateFinished"], value);
            }
            remove
            {
                this.eventTable["UpdateFinished"] = (UpdateFinishHandler) Delegate.Remove((UpdateFinishHandler) this.eventTable["UpdateFinished"], value);
            }
        }

        public event f0_0 UpdateUnfinished
        {
            add
            {
                this.eventTable["UpdateUnfinished"] = (f0_0) Delegate.Combine((f0_0) this.eventTable["UpdateUnfinished"], value);
            }
            remove
            {
                this.eventTable["UpdateUnfinished"] = (f0_0) Delegate.Remove((f0_0) this.eventTable["UpdateUnfinished"], value);
            }
        }

        protected virtual void _validate()
        {
        }

        protected TabItem AddNewTab(DevComponents.DotNetBar.TabControl tabContent, Control ctl, string name, string title)
        {
            TabItem item = new TabItem();
            tabContent.Tabs.Add(item);
            TabControlPanel panel = new TabControlPanel();
            tabContent.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            item.AttachedControl = panel;
            panel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            panel.TabItem = item;
            item.Name = name;
            item.Text = title;
            tabContent.CloseButtonVisible = true;
            tabContent.SelectedTab = item;
            return item;
        }

        public virtual void Clear()
        {
        }

        protected virtual int delete()
        {
            return 0;
        }

        public void Delete()
        {
            try
            {
                if (MessageBox.Show("Xo\x00e1 nh\x00e9", "Cảnh c\x00e1o", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    this.delete();
                    DeleteFinishHandler handler = (DeleteFinishHandler) this.eventTable["DeleteFinished"];
                    if (handler != null)
                    {
                        handler(this, new OperatorArgument(this.DataId, this.DataType));
                    }
                    this.Clear();
                }
            }
            catch (DeleteException exc)
            {
                f0_0 f_ = (f0_0) this.eventTable["DeleteUnfinished"];
                if (f_ != null)
                {
                    f_();
                }
                LogManager.LogException(exc);
            }
            catch (UnknownValueException exception)
            {
                exception.Operator = this;
                exception.OperatorFunctionType = OperatorFunctionType.Insert;
                UnfoundHandler handler2 = (UnfoundHandler) this.eventTable["Unfound"];
                if (handler2 != null)
                {
                    handler2(this, exception);
                }
                LogManager.LogException(exception);
            }
            catch (InvalidException exception2)
            {
                ValidateHandler handler3 = (ValidateHandler) this.eventTable["Invalid"];
                if (handler3 != null)
                {
                    handler3(((IMedbossException) exception2).Message());
                }
                LogManager.LogException(exception2);
            }
        }

        protected virtual int insert()
        {
            return 0;
        }

        public void Insert()
        {
            try
            {
                this.insert();
                InsertFinishHandler handler = (InsertFinishHandler) this.eventTable["InsertFinished"];
                if (handler != null)
                {
                    handler(this, new OperatorArgument(this.DataId, this.DataType));
                }
                this.Clear();
            }
            catch (InsertException exc)
            {
                f0_0 f_ = (f0_0) this.eventTable["InsertUnfinished"];
                if (f_ != null)
                {
                    f_();
                }
                LogManager.LogException(exc);
            }
            catch (UnknownValueException exception)
            {
                exception.Operator = this;
                exception.OperatorFunctionType = OperatorFunctionType.Insert;
                UnfoundHandler handler2 = (UnfoundHandler) this.eventTable["Unfound"];
                if (handler2 != null)
                {
                    handler2(this, exception);
                }
                LogManager.LogException(exception);
            }
            catch (InvalidException exception2)
            {
                ValidateHandler handler3 = (ValidateHandler) this.eventTable["Invalid"];
                if (handler3 != null)
                {
                    handler3(((IMedbossException) exception2).Message());
                }
                LogManager.LogException(exception2);
            }
        }

        public override void loadAC()
        {
        }

        public override void RefreshAC()
        {
        }

        protected virtual int update()
        {
            return 0;
        }

        public void Update()
        {
            try
            {
                if (MessageBox.Show("Sửa nh\x00e9", "Cảnh c\x00e1o", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    this.update();
                    UpdateFinishHandler handler = (UpdateFinishHandler) this.eventTable["UpdateFinished"];
                    if (handler != null)
                    {
                        handler(this, new OperatorArgument(this.DataId, this.DataType));
                    }
                    this.Clear();
                }
            }
            catch (UpdateException exc)
            {
                f0_0 f_ = (f0_0) this.eventTable["UpdateUnfinished"];
                if (f_ != null)
                {
                    f_();
                }
                LogManager.LogException(exc);
            }
            catch (UnknownValueException exception)
            {
                exception.Operator = this;
                exception.OperatorFunctionType = OperatorFunctionType.Insert;
                UnfoundHandler handler2 = (UnfoundHandler) this.eventTable["Unfound"];
                if (handler2 != null)
                {
                    handler2(this, exception);
                }
                LogManager.LogException(exception);
            }
            catch (InvalidException exception2)
            {
                ValidateHandler handler3 = (ValidateHandler) this.eventTable["Invalid"];
                if (handler3 != null)
                {
                    handler3(((IMedbossException) exception2).Message());
                }
                LogManager.LogException(exception2);
            }
        }

        public void Validate()
        {
            try
            {
                this._validate();
            }
            catch (InsertException exc)
            {
                f0_0 f_ = (f0_0) this.eventTable["InsertUnfinished"];
                if (f_ != null)
                {
                    f_();
                }
                LogManager.LogException(exc);
            }
            catch (UnknownValueException exception)
            {
                UnfoundHandler handler = (UnfoundHandler) this.eventTable["Unfound"];
                if (handler != null)
                {
                    handler(this, exception);
                }
                LogManager.LogException(exception);
            }
            catch (InvalidException exception2)
            {
                ValidateHandler handler2 = (ValidateHandler) this.eventTable["Invalid"];
                if (handler2 != null)
                {
                    handler2(((IMedbossException) exception2).Message());
                }
                LogManager.LogException(exception2);
            }
        }

        protected virtual int DataId
        {
            get
            {
                return 0;
            }
        }

        protected virtual Nammedia.Medboss.controls.DataType DataType
        {
            get
            {
                return Nammedia.Medboss.controls.DataType.None;
            }
        }
    }
}
