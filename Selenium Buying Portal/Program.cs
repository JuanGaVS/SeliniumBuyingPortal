// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

Console.WriteLine("Hello, World!");
Console.WriteLine("Testing Buying Portal from Selenium");

// Configura las opciones del navegador
var chromeOptions = new ChromeOptions();

//chromeOptions.AddArgument("--window-size=800,400");
// Aquí puedes agregar opciones adicionales según tus necesidades
// chromeOptions.AddArgument("--headless"); // Para ejecución sin cabeza

// Crea una instancia del controlador de Chrome
var driver = new ChromeDriver(chromeOptions);

// Navega a una URL
driver.Navigate().GoToUrl("http://buyingdev001:8080/");

// Realiza operaciones en la página

IWebElement userInput = driver.FindElement(By.Id("userName"));

// Realiza acciones en el campo de entrada
userInput.SendKeys("");

IWebElement passwordInput = driver.FindElement(By.Id("password"));

// Realiza acciones en el campo de entrada
passwordInput.SendKeys("");

IWebElement button = driver.FindElement(By.CssSelector("[data-testid='loginButton']"));

// Realiza clic en el botón
button.Click();

WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

IWebElement visibleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("MuiDrawer-root")));
//goto offers;

IWebElement menuElement = driver.FindElement(By.CssSelector($"[aria-label='open drawer']"));
menuElement.Click();

string searchText = "Buying";
IWebElement spanElement = driver.FindElement(By.XPath($"//span[text()='{searchText}']"));
spanElement.Click();

await Task.Delay(TimeSpan.FromSeconds(2));
searchText = "Item Details";
IWebElement spanElement2 = driver.FindElement(By.XPath($"//span[text()='{searchText}']"));
spanElement2.Click();

visibleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-testid='inputSearch']")));

IWebElement itemInput = driver.FindElement(By.CssSelector("[data-testid='inputSearch']"));
itemInput.SendKeys("117740");

await Task.Delay(TimeSpan.FromSeconds(5));

searchText = "Add SI Quote";
IWebElement addSIQButton = driver.FindElement(By.XPath($"//button[contains(text(),'{searchText}')]"));
addSIQButton.Click();

visibleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//h2[text()='Add SI Quote']")));

IWebElement dropdownArrow = driver.FindElement(By.CssSelector("[data-testid='si-quote-supplier']"));

Actions actions = new Actions(driver);

await Task.Delay(TimeSpan.FromSeconds(5));
// Simula la tecla de flecha abajo y luego Enter
actions
    .Click(dropdownArrow)
    .SendKeys(Keys.ArrowDown)
    .SendKeys(Keys.Enter)
    .Perform();

IWebElement siqPrice = driver.FindElement(By.CssSelector("[data-testid='si-quote-price']"));
siqPrice.SendKeys("100");

IWebElement siqQty = driver.FindElement(By.CssSelector("[data-testid='si-quote-quantity']"));
siqQty.SendKeys("15");

IWebElement siqComments = driver.FindElement(By.CssSelector("[data-testid='si-quote-comments']"));
siqComments.SendKeys("Test from Selenium");

IWebElement siqAdd = driver.FindElement(By.CssSelector("[data-testid='add-si-button']"));
siqAdd.Click();

visibleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(@class, 'cellValue') and contains(text(), '18.21 Man Made')]")));

offers:
    driver.Navigate().GoToUrl("http://buyingdev001:8080/offers");

    await Task.Delay(TimeSpan.FromSeconds(5));
    IWebElement searchButton = driver.FindElement(By.XPath($"//button[contains(text(),'Search')]"));
    searchButton.Click();

    wait.Timeout = TimeSpan.FromSeconds(90);
    visibleElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector($"button[data-testid='tab-2']")));
    visibleElement.Click();


// Cierra el navegador al finalizar
Console.WriteLine("Waiting 10 seconds before closing the brower");
await Task.Delay(TimeSpan.FromSeconds(10)); // Espera 5 segundos
driver.Quit();
