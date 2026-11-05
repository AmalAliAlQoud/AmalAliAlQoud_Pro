import 'package:firstproj/SignupPage.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'VerificationPage.dart';
import 'data/UserService2.dart';
import 'components/mytextfield.dart';
import 'HomePage.dart';

class LoginPage2 extends StatefulWidget {
  const LoginPage2({super.key});

  @override
  State<LoginPage2> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage2> {
  final GlobalKey<FormState> formKey = GlobalKey<FormState>();
  final TextEditingController usernamecontroller = TextEditingController();
  final TextEditingController passwordcontroller = TextEditingController();

  final UserService2 userService2 = UserService2();
  bool isLoading = false;

  // دالة تسجيل الدخول
  void login() async {
    if (!formKey.currentState!.validate()) {
      return;
    }

    setState(() {
      isLoading = true;
    });

    try {
      final email = usernamecontroller.text.trim();
      final password = passwordcontroller.text;

      final user = await userService2.login(email, password);

      if (!context.mounted) return;

      if (user != null) {
        // عند نجاح تسجيل الدخول المبدئي وإرسال الكود، ننتقل لصفحة التحقق مع تمرير الإيميل
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) => VerificationPage(
              email: email,
            ),
          ),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('Invalid email or password'),
            behavior: SnackBarBehavior.floating,
            duration: const Duration(seconds: 3),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(20),
            ),
            backgroundColor: Colors.black38,
          ),
        );
      }
    } catch (e) {
      if (!context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Failed to connect to server'),
          backgroundColor: Colors.red,
        ),
      );
    } finally {
      if (mounted) {
        setState(() {
          isLoading = false; // إيقاف مؤشر التحميل
        });
      }
    }
  }

  @override
  void dispose() {
    usernamecontroller.dispose();
    passwordcontroller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('First App - Login Page'),
        backgroundColor: Colors.deepPurple,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Form(
            key: formKey,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const SizedBox(height: 10),
                const Text(
                  'Welcome  login 2 ',
                  style: TextStyle(
                    fontSize: 24,
                    color: Colors.purple,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 20),

                // حقل البريد الإلكتروني
                MyTextField(
                  label: 'Enter your Email',
                  controller: usernamecontroller,
                  hint: 'Email',
                ),
                const SizedBox(height: 20),

                // حقل كلمة المرور
                MyTextField(
                  label: 'Enter password',
                  controller: passwordcontroller,
                  hint: 'Password',
                  obscureText: true,
                ),
                const SizedBox(height: 30),

                // زر تسجيل الدخول
                SizedBox(
                  width: double.infinity,
                  child: ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.deepPurple,
                      padding: const EdgeInsets.symmetric(vertical: 14),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    onPressed: isLoading ? null : login,
                    child: isLoading
                        ? const SizedBox(
                      height: 20,
                      width: 20,
                      child: CircularProgressIndicator(
                        color: Colors.white,
                        strokeWidth: 2,
                      ),
                    )
                        : const Text(
                      'Login',
                      style: TextStyle(
                        fontSize: 18,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
                const SizedBox(height: 15),

                // الانتقال لصفحة إنشاء حساب
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const Text("Don't have an account? "),
                    TextButton(
                      onPressed: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => const SignUpPage(),
                          ),
                        );
                      },
                      child: const Text(
                        'Sign Up',
                        style: TextStyle(
                          color: Colors.purple,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
      bottomNavigationBar: Container(
        color: Colors.deepPurple,
        height: 60,
      ),
    );
  }
}