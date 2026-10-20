import 'package:flutter/material.dart';

import 'components/mytextfield.dart';

class SignUpPage extends StatelessWidget {
  const SignUpPage({super.key});

  @override
  Widget build(BuildContext context) {
    final formKey = GlobalKey<FormState>();

    return Scaffold(
      appBar: AppBar(
        title: const Text('Sign Up'),
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
                const Text(
                  'Create New Account',
                  style: TextStyle(
                    fontSize: 24,
                    color: Colors.purple,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 20),

                // حقل الاسم الكامل
                const MyTextField(
                  label: 'Enter Full Name',
                  hint: 'full name',
                ),
                const SizedBox(height: 15),

                // حقل البريد الإلكتروني
                const MyTextField(
                  label: 'Enter your Email',
                  hint: 'Email',
                ),
                const SizedBox(height: 15),

                // حقل كلمة المرور
                const MyTextField(
                  label: 'Enter password',
                  hint: 'Password',
                  obscureText: true, // لإخفاء الحروف تلقائياً
                ),
                const SizedBox(height: 30),

                // زر إنشاء الحساب
                SizedBox(
                  width: double.infinity,
                  child: ElevatedButton(
                    onPressed: () {
                      if (formKey.currentState!.validate()) {
                        // كود تسجيل الحساب بنجاح
                      }
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.purple,
                      padding: const EdgeInsets.symmetric(vertical: 15),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                    ),
                    child: const Text(
                      'Sign Up',
                      style: TextStyle(
                        fontSize: 18,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }



}