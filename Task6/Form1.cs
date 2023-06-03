using System.Reflection;
using lib;

namespace Task6
{
    public partial class Form1 : Form
    {

        private List<Type> classes = new List<Type>();
        private List<Type> enums = new List<Type>();
        private List<string> methods = new List<string>();

        Type currType;
        object instance;
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateclassComboBox()
        {
            this.classComboBox.Items.Clear();

            foreach (Type t in classes)
            {
                this.classComboBox.Items.Add(t.Name);
            }
        }

        private void UpdatemethodComboBox()
        {
            this.methodComboBox.Items.Clear();

            foreach (string methodName in methods)
            {
                this.methodComboBox.Items.Add(methodName);
            }
        }

        private void OpenLibrary(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DLL files (*.dll)|*.dll";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Assembly assembly = Assembly.LoadFrom(openFileDialog.SafeFileName);
                var types = assembly.GetTypes();
                foreach (Type item in types)
                {
                    if (!item.IsEnum)
                    {
                        if (!item.IsAbstract && !item.IsInterface)
                        {
                            classes.Add(item);
                        }
                    }
                    else
                    {
                        enums.Add(item);
                    }
                }
            }

            UpdateclassComboBox();

            this.classComboBox.Visible = true;
            this.copybookButton.Visible = true;
        }

        private void copybookButton_Click(object sender, EventArgs e)
        {
            var val = this.classComboBox.Text;

            if (val != "")
            {
                var type = classes.Find((t) => t.Name == val);

                currType = type;

                this.methods.Clear();

                foreach (var method in type.GetMethods())
                {
                    this.methods.Add(method.Name);
                }

                this.instance = Activator.CreateInstance(currType);

                this.methodComboBox.Items.Clear();
                this.methodComboBox.Visible = true;
                this.methodButton.Visible = true;

                UpdatemethodComboBox();
            }
            else
            {
                MessageBox.Show("Класс не выбран!");
            }
        }

        private void methodButton_Click(object sender, EventArgs e)
        {

            string methodName = this.methodComboBox.Text;

            if (methodName != "")
            {
                var methodInfo = currType.GetMethod(methodName);

                if (methodInfo == null)
                {
                    Console.WriteLine($"Метод {methodName} не найден.");
                    return;
                }

                createForm(methodInfo, methodName);
            }
            else
            {
                MessageBox.Show("Метод не выбран!");
            }

        }

        private void createForm(MethodInfo methodInfo, string methodName)
        {
            var form = new Form();
            form.Text = methodName;

            var parameters = methodInfo.GetParameters();

            var y = 10;
            foreach (var parameter in parameters)
            {
                var label = new Label();
                label.Text = parameter.Name + ":";
                label.Left = 10;
                label.Top = y;
                form.Controls.Add(label);

                if (parameter.ParameterType.IsEnum)
                {
                    var comboBox = new ComboBox();
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.Left = 110;
                    comboBox.Top = y;
                    var type = enums.Find((t) => t.Name == parameter.ParameterType.Name);
                    var values = type.GetEnumValues();
                    foreach (var value in values)
                    {
                        comboBox.Items.Add(value);
                    }
                    form.Controls.Add(comboBox);
                }
                else
                {
                    var textBox = new TextBox();
                    textBox.Left = 110;
                    textBox.Top = y;
                    form.Controls.Add(textBox);
                }

                y += 30;
            }


            var button = new Button();
            button.Text = "Выполнить";
            button.Left = 110;
            button.Top = y;
            button.Click += (sender, e) =>
            {
                var args = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType.IsEnum)
                    {
                        var text = form.Controls[i * 2 + 1].Text;
                        var type = enums.Find((t) => t.Name == parameters[i].ParameterType.Name);
                        var value = Enum.Parse(parameters[i].ParameterType, text, true);
                        args[i] = value;
                    }
                    else
                    {
                        args[i] = Convert.ChangeType(form.Controls[i * 2 + 1].Text, parameters[i].ParameterType);
                    }
                }

                try
                {

                    object curInstance = null;

                    if (!methodInfo.IsStatic)
                    {
                        curInstance = this.instance;
                    }
                    var vap = methodInfo.Invoke(curInstance, args);
                    if (vap != null)
                    {
                        MessageBox.Show(vap.ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка!");
                }
            };

            form.Controls.Add(button);

            form.ShowDialog();
        }
    }
}
